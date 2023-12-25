﻿using BlazorComponent.I18n;
using SwashbucklerDiary.Rcl.Essentials;
using System.Globalization;

namespace SwashbucklerDiary.Rcl.Services
{
    public class I18nService : II18nService
    {
        private I18n I18n { get; set; } = default!;

        public CultureInfo Culture => I18n.Culture;

        public Dictionary<string, string> Languages { get; }

        public event Action? OnChanged;

        public I18nService(IStaticWebAssets staticWebAssets) 
        {
            Languages = staticWebAssets.ReadJsonAsync<Dictionary<string, string>>("json/i18n/languages.json").Result;
        }

        public void Initialize(object i18n)
        {
            I18n = (I18n)i18n;
        }

        public void SetCulture(string culture)
        {
            I18n.SetCulture(new CultureInfo(culture));
            OnChanged?.Invoke();
        }

        public string T(string? key) => T(key, true) ?? key ?? string.Empty;

        public string? T(string? key, bool whenNullReturnKey)
        {
            if (key == null)
            {
                return string.Empty;
            }

            if (I18n is null)
            {
                return string.Empty;
            }

            return I18n.T(key, whenNullReturnKey);
        }

        public string ToWeek(DateTime? dateTime = null)
        {
            return T("Week." + ((int)(dateTime ?? DateTime.Now).DayOfWeek).ToString());
        }

    }
}
