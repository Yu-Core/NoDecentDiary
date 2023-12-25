﻿using BlazorComponent;
using Microsoft.AspNetCore.Components;
using SwashbucklerDiary.Shared;

namespace SwashbucklerDiary.Rcl.Components
{
    public partial class DateFilterDialog : DialogComponentBase
    {
        private bool _visible;

        private bool showMinDate;

        private bool showMaxDate;

        private DateFilterForm internalForm = new();

        [Parameter]
        public override bool Visible
        {
            get => _visible;
            set => SetVisible(value);
        }

        [Parameter]
        public DateFilterForm Value { get; set; } = new();
        
        [Parameter]
        public EventCallback<DateFilterForm> ValueChanged { get; set; }

        [Parameter]
        public EventCallback OnOK { get; set; }

        [Parameter]
        public EventCallback OnReset { get; set; }

        protected virtual Task DialogAfterRenderAsync()
        {
            internalForm = Value.DeepCopy();
            return Task.CompletedTask;
        }

        private static Dictionary<string, DateOnly> DefaultDates 
            => DateFilterForm.DefaultDates;

        private StringNumber DefaultDate
        {
            get => internalForm.DefaultDate;
            set
            {
                internalForm.DefaultDate = value?.ToString() ?? string.Empty;
            }
        }

        private DateOnly MinDate
        {
            get => internalForm.MinDate;
            set => internalForm.MinDate = value;
        }

        private DateOnly MaxDate
        {
            get => internalForm.MaxDate;
            set => internalForm.MaxDate = value;
        }

        private string MinDateText
            => MinDate == DateOnly.MinValue ? I18n.T("Filter.Start time") : ((DateOnly)MinDate).ToString("yyyy-MM-dd");
        
        private string MaxDateText
            => MaxDate == DateOnly.MaxValue ? I18n.T("Filter.End time") : ((DateOnly)MaxDate).ToString("yyyy-MM-dd");
        
        private DateOnly Today => DateOnly.FromDateTime(DateTime.Now);

        private void SetVisible(bool value)
        {
            if (value != Visible)
            {
                _visible = value;
                if (value)
                {
                    internalForm = Value.DeepCopy();
                }
            }
        }

        private void SelectDeafultDate()
        {
            MinDate = DateOnly.MinValue;
            MaxDate = DateOnly.MaxValue;
        }

        private void OpenMinDateDialog()
        {
            showMinDate = true;
            DefaultDate = string.Empty;
        }

        private void OpenMaxDateDialog()
        {
            showMaxDate = true;
            DefaultDate = string.Empty;
        }

        private async Task HandleOnReset()
        {
            internalForm = new();
            Value = new();
            await ValueChanged.InvokeAsync(Value);
            await OnReset.InvokeAsync();
        }

        private async Task HandleOnOK()
        {
            await InternalVisibleChanged(false);
            Value = internalForm.DeepCopy();
            await ValueChanged.InvokeAsync(Value);
            await OnOK.InvokeAsync();
        }

    }
}
