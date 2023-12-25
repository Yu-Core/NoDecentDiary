﻿using Microsoft.AspNetCore.Components;
using SwashbucklerDiary.Rcl.Essentials;
using SwashbucklerDiary.Rcl.Services;
using SwashbucklerDiary.Shared;

namespace SwashbucklerDiary.Rcl.Components
{
    public abstract class MyComponentBase : ComponentBase
    {
        [Inject]
        protected INavigateService NavigateService { get; set; } = default!;

        [Inject]
        protected II18nService I18n { get; set; } = default!;

        [Inject]
        protected IAlertService AlertService { get; set; } = default!;

        [Inject]
        protected IThemeService ThemeService { get; set; } = default!;

        [Inject]
        protected IAchievementService AchievementService { get; set; } = default!;

        [Inject]
        protected IPreferences Preferences { get; set; } = default!;

        [Inject]
        protected NavigationManager Navigation { get; set; } = default!;

        [CascadingParameter(Name = "IsDark")]
        public bool Dark { get; set; }

        protected bool Light => !Dark;

        protected Task ToDo()
        {
            return AlertService.Info(I18n.T("ToDo.Title"), I18n.T("ToDo.Content"));
        }

        protected void To(string url, bool cacheCurrentURL = true)
        {
            NavigateService.PushAsync(url, cacheCurrentURL);
        }

        protected virtual async Task HandleAchievements(Achievement type)
        {
            var messages = await AchievementService.UpdateUserState(type);
            await AlertAchievements(messages);
        }

        protected async Task AlertAchievements(List<string> messages)
        {
            bool achievementsAlert = await Preferences.Get<bool>(Setting.AchievementsAlert);
            if (!achievementsAlert)
            {
                return;
            }

            foreach (var item in messages)
            {
                await AlertService.Info(I18n.T("Achievement.AchieveAchievements"), I18n.T(item));
            }
        }
    }
}
