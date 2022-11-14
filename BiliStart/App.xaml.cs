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
using BiliStart.Services;
using BiliStart.ViewModels;
using BiliStart.ViewModels.DialogViewModel;
using BiliStart.ViewModels.PageViewModels;
using BiliStart.Views;
using CommunityToolkit.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel.Resources.Core;

namespace BiliStart;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
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

    public static WindowEx MainWindow { get; } = new MainWindow();

    public App()
    {
        InitializeComponent();
        this.UnhandledException += App_UnhandledException1;
        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers
            services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

            // Services
            services.AddSingleton<IAppNotificationService, AppNotificationService>();
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();


            services.AddTransient<INavigationViewService, NavigationViewService>();
            services.AddSingleton<INavigationService, NavigationService>();


            services.AddTransient<IHotNavigationViewService, HotNavigationViewService>();
            services.AddSingleton<IHotNavigationService, HotNavigationService>();

            services.AddSingleton<IActivationService, ActivationService>();


            // 页面文件服务
            services.AddSingleton<IPageService, PageService>();

            //弹窗服务
            services.AddSingleton<ITipShow,TipShow>();

            // 文件操作
            services.AddSingleton<IFileService, FileService>();

            // 视图和视图模型
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<ShellPage>();
            services.AddSingleton<ShellViewModel>();


            services.AddTransient<LoginDialogViewModel>(); 
            services.AddTransient<LoginDialog>();

            services.AddSingleton<HomePage>();
            services.AddSingleton<HomeViewModel>();

            services.AddSingleton<PlayerPage>();
            services.AddSingleton<PlayerViewModel>();

            services.AddSingleton<HotPage>();
            services.AddSingleton<HotViewModel>();

            services.AddSingleton<MainPage>();
            services.AddSingleton<MainViewModel>();

            services.AddSingleton<TopMorePage>();
            services.AddSingleton<TopMoreViewModel>();

            services.AddSingleton<RankPage>();
            services.AddSingleton<RankViewModel>();

            services.AddSingleton<WeekPage>();
            services.AddSingleton<WeekViewModel>();
            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();
        App.GetService<IAppNotificationService>().Initialize();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException1(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        
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
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
        e.Handled= true;
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        ResourceContext resourceContext = ResourceContext.GetForViewIndependentUse();
        ResourceMap resourceMap = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
        // Here you load the resource you need
        var resourceValue = resourceMap.GetValue("AppNotificationSamplePayload", resourceContext);

        App.GetService<IAppNotificationService>().Show(string.Format(resourceValue.ValueAsString, AppContext.BaseDirectory));

        await App.GetService<IActivationService>().ActivateAsync(args);

    }
}
