using System.Collections.Specialized;
using System.Web;

using BiliStart.Contracts.Services;
using BiliStart.ViewModels;

using Microsoft.Windows.AppNotifications;

namespace BiliStart.Notifications;

public class AppNotificationService : IAppNotificationService
{
    private readonly INavigationService _navigationService;
    private readonly IHotNavigationService _hotNavigationService;

    public AppNotificationService(INavigationService navigationService,IHotNavigationService hotNavigationService)
    {
        _navigationService = navigationService;
        _hotNavigationService = hotNavigationService;
    }

    ~AppNotificationService()
    {
        //关闭服务
        Unregister();
    }

    public void Initialize()
    {
        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;

        AppNotificationManager.Default.Register();
    }

    public void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        //实验跳转
        if (ParseArguments(args.Argument)["action"] == "Settings")
        {
            App.MainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
            });
        }
    }

    public bool Show(string payload)
    {
        var appNotification = new AppNotification(payload);

        AppNotificationManager.Default.Show(appNotification);

        return appNotification.Id != 0;
    }

    public NameValueCollection ParseArguments(string arguments)
    {
        return HttpUtility.ParseQueryString(arguments);
    }

    public void Unregister()
    {
        AppNotificationManager.Default.Unregister();
    }
}
