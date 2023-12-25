﻿using CommunityToolkit.Maui;
using MauiBlazorToolkit.Extensions;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Logging;
using SwashbucklerDiary.Maui.BlazorWebView;
using SwashbucklerDiary.Maui.Extensions;

namespace SwashbucklerDiary.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiBlazorToolkit(options =>
                {
                    options.TitleBar = true;
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                })
                .ConfigureEssentials(essentials =>
                {
                    essentials.UseVersionTracking();
                }); ;

            builder.Services.AddMauiBlazorWebView();

            builder.Services.ConfigureMauiHandlers(delegate (IMauiHandlersCollection handlers)
            {
                handlers.AddHandler<IBlazorWebView>((IServiceProvider _) => new MauiBlazorWebViewHandler());
            });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSerilogConfig();

            builder.Services.AddMasaBlazorConfig();

            builder.Services.AddSqlsugarConfig();

            builder.Services.AddDependencyInjection();

            var app = builder.Build();

            app.Services.AddMauiExceptionHandle();

            return app;
        }
    }
}