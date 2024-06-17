﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SwashbucklerDiary.Rcl.Components;
using SwashbucklerDiary.Rcl.Essentials;
using SwashbucklerDiary.Rcl.Extensions;
using SwashbucklerDiary.Rcl.Models;
using SwashbucklerDiary.Rcl.Services;
using SwashbucklerDiary.Shared;
using System.Globalization;

namespace SwashbucklerDiary.Rcl.Pages
{
    public partial class MinePage : ImportantComponentBase
    {
        private string? language;

        private Theme theme;

        private string? userName;

        private string? sign;

        private string? avatar;

        private string? privacyModeEntrancePassword;

        private bool showLanguage;

        private bool showTheme;

        private bool showFeedback;

        private bool showPreviewImage;

        private bool privacyMode;

        private bool hidePrivacyModeEntrance;

        private bool showprivacyModeEntrancePasswordDialog;

        private int diaryCount;

        private long wordCount;

        private int activeDayCount;

        private int imageCount;

        private int audioCount;

        private int videoCount;

        private ScrollContainer scrollContainer = default!;

        private static readonly Dictionary<string, Theme> themeItems = new()
        {
            { "Theme.System", Theme.System },
            { "Theme.Light", Theme.Light },
            { "Theme.Dark", Theme.Dark },
        };

        private Dictionary<string, List<DynamicListItem>> ViewLists = [];

        private List<DynamicListItem> FeedbackTypes = [];

        private Dictionary<string, string> FeedbackTypeDatas = [];

        [Inject]
        protected IDiaryService DiaryService { get; set; } = default!;

        [Inject]
        protected IAccessExternal AccessExternal { get; set; } = default!;

        [Inject]
        protected ILogger<MinePage> Logger { get; set; } = default!;

        [Inject]
        protected IThemeService ThemeService { get; set; } = default!;

        [Inject]
        protected IResourceService ResourceService { get; set; } = default!;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            LoadView();
            I18n.OnChanged += I18nChange;
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadViewAsync();

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await UpdateStatisticalDataAsync();

                StateHasChanged();
            }
        }

        protected override async Task OnResume()
        {
            await UpdateStatisticalDataAsync();
            await base.OnResume();
        }

        protected override void OnDispose()
        {
            I18n.OnChanged -= I18nChange;
            base.OnDispose();
        }

        protected override void ReadSettings()
        {
            base.ReadSettings();

            language = SettingService.Get<string>(Setting.Language);
            userName = SettingService.Get<string?>(Setting.UserName, null);
            sign = SettingService.Get<string?>(Setting.Sign, null);
            theme = (Theme)SettingService.Get<int>(Setting.Theme);
            avatar = SettingService.Get<string>(Setting.Avatar);
            privacyMode = SettingService.GetTemp<bool>(TempSetting.PrivacyMode);
            hidePrivacyModeEntrance = SettingService.Get<bool>(Setting.HidePrivacyModeEntrance);
            privacyModeEntrancePassword = SettingService.Get<string>(Setting.PrivacyModeEntrancePassword);
        }

        private WordCountStatistics WordCountType
            => (WordCountStatistics)Enum.Parse(typeof(WordCountStatistics), I18n.T("Write.WordCountType")!);

        private void LoadView()
        {
            ViewLists = new()
            {
                {
                    "Mine.Data",
                    new()
                    {
                        new(this, "Mine.Backups","mdi-folder-sync-outline",() => To("backups")),
                        new(this, "Mine.Export","mdi-export",() => To("export")),
                        new(this, "PrivacyMode.Name","mdi-hexagon-slice-3",TryToPrivacyMode,()=>!hidePrivacyModeEntrance || privacyMode),
                    }
                },
                {
                    "Mine.Settings",
                    new()
                    {
                        new(this,"Mine.Settings","mdi-cog-outline",() => To("setting")),
                        new(this,"Mine.Languages","mdi-web",() => showLanguage = true),
                        new(this,"Mine.Night","mdi-weather-night",() => showTheme = true),
                    }
                },
                {
                    "Mine.Function",
                    new()
                    {
                        new(this, "Mine.Achievement.Name","mdi-trophy-outline",() => To("achievement")),
                        new(this, "Location.Name","mdi-map-marker-outline",() => To("locationSetting")),
                    }
                },
                {
                    "Mine.Other",
                    new()
                    {
                        new(this,"Mine.Feedback","mdi-email-outline",() => showFeedback = true),
                        new(this,"Mine.About","mdi-information-outline",() => To("about")),
                    }
                }
            };
            FeedbackTypes =
            [
                new(this, "Email","mdi-email-outline",SendMail),
                new(this, "Github","mdi-github",ToGithub),
                new(this, "QQGroup","mdi-qqchat",OpenQQGroup),
            ];
        }

        private async Task LoadViewAsync()
        {
            FeedbackTypeDatas = await StaticWebAssets.ReadJsonAsync<Dictionary<string, string>>("json/feedback-type/feedback-type.json");
        }

        private async Task LanguageChanged(string value)
        {
            I18n.SetCulture(value);
            await SettingService.Set(Setting.Language, value);
        }

        private async Task SendMail()
        {
            var mail = FeedbackTypeDatas["Email"];
            try
            {
                bool isSuccess = await PlatformIntegration.SendEmail(null, null, [mail]);
                if (isSuccess)
                {
                    return;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e, "SendMailFail");
            }

            await PlatformIntegration.SetClipboard(mail);
            await AlertService.Success(I18n.T("Mine.MailCopy"));
        }

        private async Task ToGithub()
        {
            var githubUrl = FeedbackTypeDatas["Github"];
            await PlatformIntegration.OpenBrowser(githubUrl);
        }

        private async Task ThemeChanged(Theme value)
        {
            await Task.WhenAll(
                ThemeService.SetThemeAsync(value),
                SettingService.Set(Setting.Theme, (int)value));
        }

        private async Task OpenQQGroup()
        {
            try
            {
                bool isSuccess = await AccessExternal.JoinQQGroup();
                if (isSuccess)
                {
                    return;
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e, "JoinQQGroupError");
            }

            var qqGroup = FeedbackTypeDatas["QQGroup"];
            await PlatformIntegration.SetClipboard(qqGroup);
            await AlertService.Success(I18n.T("Mine.QQGroupCopy"));
        }

        private Task Search(string? value)
        {
            To($"searchAppFunction?query={value}");
            return Task.CompletedTask;
        }

        private async Task UpdateStatisticalDataAsync()
        {
            List<Task> tasks = [];
            tasks.Add(UpdateDiaryStatisticalDataAsync());
            tasks.Add(UpdateResourceStatisticalDataAsync());
            await Task.WhenAll(tasks);
        }

        private async Task UpdateDiaryStatisticalDataAsync()
        {
            var diries = await DiaryService.QueryAsync();
            diaryCount = diries.Count;
            wordCount = diries.GetWordCount(WordCountType);
            activeDayCount = diries.Select(it => DateOnly.FromDateTime(it.CreateTime)).Distinct().Count();
        }

        private async Task UpdateResourceStatisticalDataAsync()
        {
            var resources = await ResourceService.QueryAsync();
            imageCount = resources.Count(it => it.ResourceType == MediaResource.Image);
            audioCount = resources.Count(it => it.ResourceType == MediaResource.Audio);
            videoCount = resources.Count(it => it.ResourceType == MediaResource.Video);
        }

        private async void I18nChange(CultureInfo _)
        {
            var diries = await DiaryService.QueryAsync();
            wordCount = diries.GetWordCount(WordCountType);
            await InvokeAsync(StateHasChanged);
        }

        private void TryToPrivacyMode()
        {
            if (privacyMode || string.IsNullOrEmpty(privacyModeEntrancePassword))
            {
                ToPrivacyMode();
            }
            else
            {
                showprivacyModeEntrancePasswordDialog = true;
            }
        }

        private void ToPrivacyMode()
        {
            SettingService.SetTemp<bool>(TempSetting.AllowEnterPrivacyMode, true);
            To("privacyMode");
        }

        private async Task InputPassword(string value)
        {
            showprivacyModeEntrancePasswordDialog = false;
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            string salt = Setting.PrivacyModeEntrancePassword.ToString();
            if (privacyModeEntrancePassword != (value + salt).MD5Encrytp32())
            {
                await AlertService.Error(I18n.T("PrivacyMode.PasswordError"));
                return;
            }

            ToPrivacyMode();
        }
    }
}
