﻿using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SwashbucklerDiary.Rcl.Essentials;
using SwashbucklerDiary.Rcl.Models;
using SwashbucklerDiary.Rcl.Services;
using SwashbucklerDiary.Shared;
using System.Globalization;
using Theme = SwashbucklerDiary.Shared.Theme;

namespace SwashbucklerDiary.Rcl.Layout
{
    public partial class MainLayoutBase : LayoutComponentBase, IDisposable
    {
        protected bool afterInitSetting;

        protected readonly List<NavigationButton> navigationButtons = [
            new("Main.Diary", "mdi-notebook-outline", "mdi-notebook", ""),
            new("Main.History", "mdi-clock-outline", "mdi-clock", "history"),
            new("Main.FileBrowse", "mdi-file-outline", "mdi-file", "fileBrowse"),
            new("Main.Mine",  "mdi-account-outline", "mdi-account", "mine"),
        ];

        protected IEnumerable<string> permanentPaths = [];

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected INavigateService NavigateService { get; set; } = default!;

        [Inject]
        protected II18nService I18n { get; set; } = default!;

        [Inject]
        protected ISettingService SettingService { get; set; } = default!;

        [Inject]
        protected IPopupService PopupService { get; set; } = default!;

        [Inject]
        protected IAlertService AlertService { get; set; } = default!;

        [Inject]
        protected IThemeService ThemeService { get; set; } = default!;

        [Inject]
        protected IVersionUpdataManager VersionUpdataManager { get; set; } = default!;

        [Inject]
        protected MasaBlazor MasaBlazor { get; set; } = default!;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = default!;

        public void Dispose()
        {
            OnDispose();
            GC.SuppressFinalize(this);
        }

        protected bool IsPermanentPath
            => permanentPaths.Any(it => it == NavigationManager.GetAbsolutePath());

        protected override void OnInitialized()
        {
            base.OnInitialized();
            LoadView();
            AlertService.Initialize(PopupService);
            I18n.OnChanged += LanguageChanged;
            permanentPaths = navigationButtons.Select(it => NavigationManager.ToAbsoluteUri(it.Href).AbsolutePath).ToList();
        }

        protected Task InitNavigateServiceAsync()
        {
            return NavigateService.Init(NavigationManager, JSRuntime, permanentPaths);
        }

        protected virtual void OnDispose()
        {
            I18n.OnChanged -= LanguageChanged;
        }

        protected virtual async Task InitSettingsAsync()
        {
            await SettingService.InitializeAsync();
            afterInitSetting = true;
            var timeout = SettingService.Get<int>(Setting.AlertTimeout);
            AlertService.SetTimeout(timeout);
            await InitNavigateServiceAsync();
        }

        protected void LoadView()
        {
            foreach (var button in navigationButtons)
            {
                button.OnClick = () => NavigationManager.NavigateTo(button.Href, replace: true);
            }
        }

        protected virtual void ThemeChanged(Theme theme)
        {
            MasaBlazor.SetTheme(theme == Theme.Dark);
        }

        protected async void LanguageChanged(CultureInfo cultureInfo)
        {
            StateHasChanged();
            await JSRuntime.InvokeVoidAsync("setLanguage", cultureInfo.Name);
        }
    }
}
