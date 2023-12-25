﻿using SwashbucklerDiary.Rcl.Components;

namespace SwashbucklerDiary.Rcl.Pages
{
    public partial class UserAgreementPage : ImportantComponentBase
    {
        private string? content { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadingData();
            await base.OnInitializedAsync();
        }

        private async Task LoadingData()
        {
            var uri = I18n.T("FilePath.UserAgreement")!;
            content = await StaticWebAssets.ReadContentAsync(uri);
        }
    }
}
