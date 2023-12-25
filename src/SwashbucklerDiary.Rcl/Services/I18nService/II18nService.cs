﻿using SwashbucklerDiary.Rcl.Models;
using System.Globalization;

namespace SwashbucklerDiary.Rcl.Services
{
    /// <summary>
    /// 为了解决MAUI与Blazor中作用域不同，导致获取的I18n实例不同
    /// </summary>
    public interface II18nService
    {
        event Action OnChanged;

        CultureInfo Culture { get; }

        Dictionary<string,string> Languages { get; }

        void Initialize(object i18n);

        string T(string? key);

        string? T(string? key, bool whenNullReturnKey);

        void SetCulture(string culture);

        string ToWeek(DateTime? dateTime = null);
    }
}
