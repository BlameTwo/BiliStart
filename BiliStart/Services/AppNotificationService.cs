using System;
using System.Collections.Specialized;
using System.Web;

using BiliStart.Contracts.Services;
using BiliStart.ViewModels;
using CommunityToolkit.WinUI.Notifications;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.Windows.AppNotifications;
using Windows.ApplicationModel.Activation;
using Windows.System;
using Windows.UI.Popups;
using static QRCoder.PayloadGenerator;

namespace BiliStart.Notifications;

public class AppNotificationService : IAppNotificationService
{
    private readonly INavigationService _navigationService;
    private readonly IHotNavigationService _hotNavigationService;

    public ITipShow TipShow
    {
        get;
    }

    public AppNotificationService(INavigationService navigationService,IHotNavigationService hotNavigationService,ITipShow tipShow)
    {
        _navigationService = navigationService;
        _hotNavigationService = hotNavigationService;
        TipShow = tipShow;
        ContentDialog contentDialog = new ContentDialog();
            
    }

    ~AppNotificationService()
    {
        //关闭服务
        Unregister();
    }

    public void Initialize()
    {
        AppNotificationManager.Default.NotificationInvoked += OnNotificationInvoked;

        //AppNotificationManager.Default.Register();
    }

    public void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        switch (ParseArguments(args.Argument)["action"])
        {
            case "Settings":
                App.MainWindow.DispatcherQueue.TryEnqueue(() =>
                {
                    _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
                });
                break;
            case "primary":
                App.MainWindow.DispatcherQueue.TryEnqueue(() =>
                {
                    _navigationService.NavigateTo(typeof(HomeViewModel).FullName!);
                });
                break;
            case "secondary":
                App.MainWindow.DispatcherQueue.TryEnqueue(() =>
                {
                    _navigationService.NavigateTo(typeof(HotViewModel).FullName!);
                });
                break;
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

    public void CreateShow(string arguments,string PrimaryText, string SecondaryText, string Title, string SubTitle, string LeftImage = "")
    {
        if (!AppNotificationManager.IsSupported())
            TipShow.SendMessage(null,"您的计算机不支持当前通知");
        if (LeftImage != null)
        {

            string xml = $"<toast lang=\"zh-CN\" launch=\"action=ToastClick\">" +
                            "<visual>" +
                                "<binding template=\"ToastGeneric\">" +
                                   $"<image placement=\"appLogoOverride \" src=\"{LeftImage}\" hint-crop=\"circle\" />" +
                                   $"<text>{Title}</text>" +
                                   $"<text>{SubTitle}</text>" +
                                "</binding>" +
                            "</visual>" +
                            "<actions>"+
                                "<action"+
                                $" content=\"{PrimaryText}\"" + $" arguments=\"action=primary\"" +" >"+
                                "</action>"+
                                 "<action" +
                                $" content=\"{SecondaryText}\"" + $" arguments=\"action=secondary\"" + " >" +
                                "</action>" +
                            "</actions>" +
                         "</toast>";
            var appNotification = new AppNotification(xml);
            AppNotificationManager.Default.Show(appNotification);
        }
    }
}
