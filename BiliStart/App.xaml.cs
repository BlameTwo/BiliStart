﻿using BiliBiliAPI;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Models.Settings;
using BiliStart.Activation;
using BiliStart.Contracts.Services;
using BiliStart.Core.Contracts.Services;
using BiliStart.Core.Services;
using BiliStart.Dialogs;
using BiliStart.Models;
using BiliStart.Notifications;
using BiliStart.Pages;
using BiliStart.Pages.Dynamics;
using BiliStart.Services;
using BiliStart.Styles.Search;
using BiliStart.ViewModels;
using BiliStart.ViewModels.DialogViewModel;
using BiliStart.ViewModels.PageViewModels;
using BiliStart.ViewModels.PageViewModels.DynamicsViewModels;
using BiliStart.ViewModels.SearchModels;
using BiliStart.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel.Resources.Core;

namespace BiliStart;
public partial class App : Application
{
    private ShellPage _shell;

    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static bool IsLogin
    {
        get; set;
    } = false;

    public static Window MainWindow { get; } = new MainWindow();

    public App()
    {
        InitializeComponent();
        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

            services.AddSingleton<IAppNotificationService, AppNotificationService>();

            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();


            //应用导航
            services.AddSingleton<IAppNavigationService, AppNavigationService>();
            services.AddTransient<IAppNavigationViewService, AppNavigationViewService>();


            services.AddSingleton<IActivationService, ActivationService>();


            // 页面文件服务
            services.AddSingleton<IPageService, PageService>();

            //弹窗服务
            services.AddSingleton<ITipShow,TipShow>();

            // 文件操作
            services.AddSingleton<IFileService, FileService>();

            //跳转服务
            services.AddSingleton<IGoVideo,GoVideo>();

            //window窗体管理服务
            services.AddSingleton<IWindowManager, Services.WindowManager>();

            //导入grpc请求服务
            services.AddSingleton<BiliBiliAPI.Grpc.Interface.IHttpProvider, BiliBiliAPI.Grpc.Service.HttpProvider>();
            services.AddSingleton<BiliBiliAPI.Grpc.Interface.IDefaultRequestHeader, BiliBiliAPI.Grpc.Service.DefaultRequestHeader>();

            // 视图和视图模型
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<ShellPage>();
            services.AddSingleton<ShellViewModel>();
            services.AddSingleton<MainPage>();
            services.AddSingleton<MainViewModel>();

            services.AddTransient<LoginDialogViewModel>(); 
            services.AddTransient<LoginDialog>();

            services.AddTransient<HomePage>();
            services.AddTransient<HomeViewModel>();

            services.AddTransient<PlayerPage>();
            services.AddTransient<PlayerViewModel>();

            services.AddTransient<HotPage>();
            services.AddTransient<HotViewModel>();


            services.AddTransient<TopMorePage>();
            services.AddTransient<TopMoreViewModel>();

            services.AddTransient<RankPage>();
            services.AddTransient<RankViewModel>();

            services.AddTransient<WeekPage>();
            services.AddTransient<WeekViewModel>();

            services.AddTransient<SearchPage>();
            services.AddTransient<SearchViewModel>();

            services.AddTransient<Styles.Search.SearchVideo>();
            services.AddTransient<SearchVideoViewModel>();

            services.AddTransient<SearchAnimation>();
            services.AddTransient<SearchAnimationViewModel>();

            services.AddTransient<SearchMovie>();
            services.AddTransient<SearchMovieViewModel>();

            services.AddTransient<MusicAllPage>();
            services.AddTransient<MusicAllViewModel>();

            services.AddTransient<MustWatchPage>();
            services.AddTransient<MustWatchViewModel>();

            services.AddTransient <DynamicPage>();
            services.AddTransient<DynamicViewModel>();

            services.AddTransient<MyInfoPage>();
            services.AddTransient<MyInfoViewModel>();

            services.AddTransient<PGCPlayerPage>();
            services.AddTransient<PGCPlayerViewModel>();

            services.AddTransient<VideoPlayerPage>();
            services.AddTransient<VideoPlayerViewModel>();

            services.AddTransient<HistoryPage>();
            services.AddTransient<HistoryViewModel>();
            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();
        App.GetService<IAppNotificationService>().Initialize();

        UnhandledException += App_UnhandledException;
    }


    public static AccountToken Token
    {
        get
        {
            try
            {
                var result= AccountSettings.Read();

                if (result== null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        e.Handled= true;
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        ResourceContext resourceContext = ResourceContext.GetForViewIndependentUse();

        //App.GetService<IAppNotificationService>().CreateShow("ToGO!","前往首页","前往热门","欢迎使用Bili","你好！",AppContext.BaseDirectory+ "Assets/icon.ico");

        await App.GetService<IActivationService>().ActivateAsync(args);

    }
}
