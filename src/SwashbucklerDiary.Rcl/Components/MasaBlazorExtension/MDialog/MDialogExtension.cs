﻿using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using SwashbucklerDiary.Rcl.Essentials;

namespace SwashbucklerDiary.Rcl.Components
{
    //The significance of extension is to enable dialog or similar components to support return keys
    public class MDialogExtension : MDialog
    {
        [Inject]
        protected INavigateService NavigateService { get; set; } = default!;

        [Parameter]
        public bool MyValue
        {
            get => base.Value;
            set => SetValue(value);
        }

        [Parameter]
        public EventCallback<bool> MyValueChanged
        {
            get => base.ValueChanged;
            set => base.ValueChanged = value;
        }

        protected override ValueTask DisposeAsyncCore()
        {
            if (Value)
            {
                NavigateService.RemoveHistoryAction(Close);
            }

            return base.DisposeAsyncCore();
        }

        private void SetValue(bool value)
        {
            if (base.Value == value)
            {
                return;
            }

            base.Value = value;
            if (value)
            {
                NavigateService.AddHistoryAction(Close);
            }
            else
            {
                NavigateService.RemoveHistoryAction(Close);
            }
        }

        private async void Close()
        {
            MyValue = false;
            if (MyValueChanged.HasDelegate)
            {
                await MyValueChanged.InvokeAsync(false);
            }
        }
    }
}
