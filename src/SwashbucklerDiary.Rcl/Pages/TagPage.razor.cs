﻿using Microsoft.AspNetCore.Components;
using SwashbucklerDiary.Rcl.Components;

namespace SwashbucklerDiary.Rcl.Pages
{
    public partial class TagPage : DiariesPageComponentBase
    {
        private string? tagName;

        [Parameter]
        public Guid Id { get; set; }

        protected override async Task UpdateDiariesAsync()
        {
            var tag = await TagService.FindIncludesAsync(Id);
            tagName = tag?.Name;
            Diaries = tag?.Diaries ?? [];
        }

        private void NavigateToWrite()
        {
            NavigateService.PushAsync($"/write?tagId={Id}");
        }
    }
}
