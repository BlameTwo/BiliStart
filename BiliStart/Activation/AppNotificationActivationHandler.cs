using BiliStart.Contracts.Services;
using BiliStart.ViewModels;

using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using Microsoft.Windows.AppNotifications;

namespace BiliStart.Activation;

public class AppNotificationActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly IAppNavigationService _navigationService;
    private readonly IAppNotificationService _notificationService;

    public AppNotificationActivationHandler(IAppNavigationService navigationService, IAppNotificationService notificationService)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        return AppInstance.GetCurrent().GetActivatedEventArgs()?.Kind == ExtendedActivationKind.AppNotification;
    }

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        var activatedEventArgs = (AppNotificationActivatedEventArgs)AppInstance.GetCurrent().GetActivatedEventArgs().Data;

       
        switch (_notificationService.ParseArguments(activatedEventArgs.Argument)["action"])
        {
            case "Settings":
                App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
                {
                    _navigationService.NavigationTo(AppNavigationViewsEnum.ShellFrame,typeof(SettingsViewModel).FullName!);
                });
                break;
            case "primary":
                App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
                {
                    _navigationService.NavigationTo(AppNavigationViewsEnum.ShellFrame, typeof(HomeViewModel).FullName!);
                });
                break;
            case "secondary":
                App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
                {
                    _navigationService.NavigationTo(AppNavigationViewsEnum.ShellFrame, typeof(HotViewModel).FullName!);
                });
                break;
        }
        await Task.CompletedTask;
    }
}
