﻿using SwashbucklerDiary.Maui.Essentials;
using SwashbucklerDiary.Maui.IRepository;
using SwashbucklerDiary.Maui.Repository;
using SwashbucklerDiary.Maui.Services;
using SwashbucklerDiary.Rcl.Essentials;
using SwashbucklerDiary.Rcl.Extensions;
using SwashbucklerDiary.Rcl.Services;
using SwashbucklerDiary.Services;

namespace SwashbucklerDiary.Maui.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<IStaticWebAssets, Essentials.StaticWebAssets>();

            services.AddRclDependencyInjection();

            services.AddSingleton<IDiaryRepository, DiaryRepository>();
            services.AddSingleton<ITagRepository, TagRepository>();
            services.AddSingleton<IUserAchievementRepository, UserAchievementRepository>();
            services.AddSingleton<IUserStateModelRepository, UserStateModelRepository>();
            services.AddSingleton<ILocationRepository, LocationRepository>();
            services.AddSingleton<ILogRepository, LogRepository>();
            services.AddSingleton<IResourceRepository, ResourceRepository>();

            services.AddSingleton<ITagService, TagService>();
            services.AddSingleton<IDiaryService, DiaryService>();
            services.AddSingleton<IAchievementService, AchievementService>();
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IResourceService, ResourceService>();

            services.AddSingleton<INavigateService, NavigateService>();
            services.AddSingleton<IIconService, IconService>();
            services.AddSingleton<Rcl.Essentials.IPreferences, Essentials.Preferences>();
            services.AddSingleton<IAlertService, AlertService>();
            services.AddSingleton<II18nService, I18nService>();
            services.AddSingleton<IPlatformIntegration, PlatformIntegration>();

            services.AddSingleton<IAppLifecycle, AppLifecycle>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<IAppFileManager, Essentials.AppFileManager>();
            services.AddSingleton<IAccessExternal, Services.AccessExternal>();
            services.AddSingleton<IMediaResourceManager, MediaResourceManager>();
            services.AddSingleton<IDiaryFileManager, Services.DiaryFileManager>();
            services.AddSingleton<IAvatarService, AvatarService>();
            services.AddScoped<ScreenshotJSModule>();
            services.AddScoped<Rcl.Essentials.IScreenshot, Essentials.Screenshot>();
            services.AddSingleton<IStorageSpace, StorageSpace>();
            services.AddSingleton<IVersionUpdataManager, VersionUpdataManager>();
            services.AddSingleton<IWebDAV, Essentials.WebDAV>();

            return services;
        }
    }
}
