﻿using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using SwashbucklerDiary.Extend;
using SwashbucklerDiary.Models;

namespace SwashbucklerDiary.Components
{
    public partial class WebDavConfigDialog : FocusDialogComponentBase
    {
        private MForm? MForm;
        private WebDavConfigForm configModel = new();
        private bool showPassword;

        [Parameter]
        public WebDavConfigForm WebDavConfigForm { get; set; } = default!;
        [Parameter]
        public EventCallback<WebDavConfigForm> OnOK { get; set; }

        protected override async Task DialogAfterShowContent()
        {
            MForm?.Reset();
            configModel = WebDavConfigForm.DeepCopy();
            await base.DialogAfterShowContent();
        }

        private async Task HandleOnOK()
        {
            await OnOK.InvokeAsync(configModel);
        }

        
    }
}
