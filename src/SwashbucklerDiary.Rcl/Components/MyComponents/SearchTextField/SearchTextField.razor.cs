﻿using Microsoft.AspNetCore.Components;

namespace SwashbucklerDiary.Rcl.Components
{
    public partial class SearchTextField : OpenCloseComponentBase, IDisposable
    {
        private bool _visible;

        [Parameter]
        public override bool Visible
        {
            get => _visible;
            set => SetValue(value);
        }
        
        [Parameter]
        public string? Value { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<string> OnChanged { get; set; }

        [Parameter]
        public string? Title { get; set; }

        [Parameter]
        public string? Placeholder { get; set; }

        private bool ShowTitle => !string.IsNullOrEmpty(Title);

        private string? TextFieldColor => Light ? "grey" : null;

        private void SetValue(bool value)
        {
            if (_visible != value)
            {
                _visible = value;
                if (!ShowTitle)
                {
                    return;
                }

                Task.Run(() =>
                {
                    if (value)
                    {
                        NavigateService.Action += CloseSearch;
                    }
                    else
                    {
                        Value = string.Empty;
                        NavigateService.Action -= CloseSearch;
                    }
                });
            }
        }

        public void Dispose()
        {
            NavigateService.Action -= CloseSearch;
            GC.SuppressFinalize(this);
        }

        private async void CloseSearch()
        {
            await InternalVisibleChanged(false);
        }

        private async Task InternalSearchChanged(string? value)
        {
            Value = value;
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(Value);
            }
            if (OnChanged.HasDelegate)
            {
                await OnChanged.InvokeAsync(Value);
            }
        }
    }
}
