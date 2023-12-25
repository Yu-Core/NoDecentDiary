﻿using Microsoft.AspNetCore.Components.WebView;

namespace SwashbucklerDiary.Maui
{
    public partial class MainPage
    {
        private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e)
        {
            e.Configuration.AllowsInlineMediaPlayback = true;
            //e.Configuration.MediaTypesRequiringUserActionForPlayback = WebKit.WKAudiovisualMediaTypes.None;
        }

        private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e)
        {
            e.WebView.ScrollView.ShowsVerticalScrollIndicator = false; // 关闭滚动条
        }
    }
}
