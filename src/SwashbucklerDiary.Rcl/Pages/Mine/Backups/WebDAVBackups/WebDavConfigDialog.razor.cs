﻿using Masa.Blazor;
using Microsoft.AspNetCore.Components;
using SwashbucklerDiary.Rcl.Components;
using SwashbucklerDiary.Shared;

namespace SwashbucklerDiary.Rcl.Pages
{
    public partial class WebDavConfigDialog : FocusDialogComponentBase
    {
        private MForm? mForm;

        private WebDavConfigForm configModel = new();

        private bool showPassword;

        [Parameter]
        public WebDavConfigForm Form { get; set; } = default!;

        [Parameter]
        public EventCallback<WebDavConfigForm> OnOK { get; set; }

        protected override async Task DialogAfterRenderAsync()
        {
            mForm?.Reset();
            configModel = Form.DeepCopy();
            await base.DialogAfterRenderAsync();
        }

        private async Task HandleOnOK()
        {
            await OnOK.InvokeAsync(configModel);
        }

        
    }
}
