using BiliBiliAPI.Models.Videos;
using BiliStart.ViewModels;
using BiliStart.ViewModels.Models;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using Windows.Media.Playback;
using BiliStart.Helpers;
using Windows.Media.Core;
using BiliStart.Contracts.Services;
using BiliBiliAPI.Models.HomeVideo;
using BiliBiliAPI.Models;
using Windows.UI.Core;
using Microsoft.UI.Xaml.Input;

namespace BiliStart.Views;
public sealed partial class PlayerPage : Microsoft.UI.Xaml.Controls.Page
{


    public PlayerViewModel ViewModel
    {
        get;
    }



    public PlayerPage()
    {
        ViewModel = App.GetService<PlayerViewModel>();
        LocalSettingsService = App.GetService<ILocalSettingsService>(); 
        this.InitializeComponent();
        Loaded += PlayerPage_Loaded;
        TitleBarText.Text = "AppDisplayName".GetLocalized();
        IsFull = false;
        Timer.Tick += Timer_Tick;
        Timer.Start();
        this.SizeChanged += PlayerPage_SizeChanged;
        process.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(UIElement_OnPointerReleased) , true );
        Close.Completed += (s, e) =>
        {
            VideoData.Visibility = Visibility.Collapsed;
            CloseStop.Begin();
        };
        Show.Completed += (s, e) =>
        {
            VideoData.Visibility = Visibility.Visible;
            ShowStop.Begin();
        };
    }

    private void UIElement_OnPointerReleased(object sender, PointerRoutedEventArgs e)
    {
        media.MediaPlayer.PlaybackSession.Position = TimeSpan.FromMilliseconds((sender as Slider)!.Value);
    }

    private void PlayerPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        TitleBar.Margin = new Thickness(BackButton.ActualWidth+5,0,0,0);
    }

    public ILocalSettingsService LocalSettingsService
    {
        get;
    }

    private void Timer_Tick(object? sender, object e)
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            ViewModel.SliderValue = media.MediaPlayer.PlaybackSession.Position.TotalMilliseconds;
        });
    }

    bool IsFull;

    bool IsPlay = false;

    DispatcherTimer Timer = new() { Interval = TimeSpan.FromSeconds(1) };

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        Loaded -= PlayerPage_Loaded;
        MediaClear();
        GC.Collect();
        GC.WaitForPendingFinalizers(); base.OnNavigatingFrom(e);
    }

    public void MediaClear()
    {
        Timer.Stop();
        ViewModel.DanmuProcessTime.Stop();
        if (ViewModel.NowMediaPlayer != null)
        {
            ViewModel.NowMediaPlayer.MediaOpened -= MediaPlayer_MediaOpened;

            ViewModel.NowMediaPlayer.Pause();
            ViewModel.NowMediaPlayer.Dispose();
        }
        Timer.Tick -= Timer_Tick;
        ViewModel.Source = null;
        ViewModel.NowMediaPlayer = null;

    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        System.GC.Collect();
    }


    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        if(e.Parameter is ViewModels.Models.PlayerArgs playerArgs) 
        {
            ViewModel.Args = playerArgs;
            if (playerArgs.Content.Pages.Count <= 1)
            {
                //PagePivotItem.Visibility = Visibility.Collapsed;
                RightPivot.Items.Remove(PagePivotItem);
            }
            ViewModel.InitVideo(playerArgs);
        }
        
    }


    private async void PlayerPage_Loaded(object sender, RoutedEventArgs e)
    {
       
        this.media.SetMediaPlayer(ViewModel.NowMediaPlayer);
        //在这里订阅一个媒体加载完毕事件
        ViewModel.NowMediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
        ViewModel.DanmakuControl = this.Danmakulist;
        //这里搞一个主题更改
        if (App.MainWindow.Content is FrameworkElement rootElement)
        {
            rootElement.ActualThemeChanged += (s, e) =>
            {
                //对颜色强制进行更改，ShellPage中的个人信息Flyout也有这样的问题，先略过。
                if(this.Content is FrameworkElement rootElement2)
                {
                    rootElement2.RequestedTheme = rootElement.ActualTheme;
                }
            };
        }
        
    }


    private void MediaPlayer_MediaOpened(Windows.Media.Playback.MediaPlayer sender, object args)
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () => InitPlay());

        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            ViewModel.MaxValue = media.MediaPlayer.PlaybackSession.NaturalDuration.TotalMilliseconds;
            
        });
    }

    async void InitPlay()
    {
        if(await LocalSettingsService.ReadSettingAsync<int>("Player_AutoStart") == 0)
        {
            IsPlay = true;
            media.MediaPlayer.Play();
        }
    }




    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (IsFull)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hwnd));
            appWindow.SetPresenter(AppWindowPresenterKind.Default);
            Grid.SetRowSpan(mediaborder, 1);
            Grid.SetRow(mediaborder, 1);
            Grid.SetRowSpan(mediaborder,2);
            media.Margin = new Thickness(5);
            mediaborder.Margin = new Thickness(10);
            App.MainWindow.SetTitleBar(TitleBar);
            MoreColumn.Width = new GridLength(350, GridUnitType.Pixel);
            bottommenu.Visibility = Visibility.Visible;
            mediaborder.CornerRadius = new CornerRadius(10);
            ViewModel.FullChanged(false);
            App.MainWindow.ExtendsContentIntoTitleBar = true;
            IsFull = false;
        }
        else
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hwnd));
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            App.MainWindow.SetTitleBar(null);
            Grid.SetRowSpan(mediaborder, 1);
            Grid.SetRow(mediaborder, 0);
            Grid.SetRowSpan(mediaborder, 2);
            mediaborder.Margin = new Thickness(0);
            media.Margin = new Thickness(0);
            MoreColumn.Width = new GridLength(0);
            bottommenu.Visibility = Visibility.Collapsed;
            mediaborder.CornerRadius = new CornerRadius(0);
            ViewModel.FullChanged(true);
            App.MainWindow.ExtendsContentIntoTitleBar = false;
            IsFull = true;
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        if (IsPlay)
        {
            media.MediaPlayer.Pause();
            IsPlay = false;
        }
        else
        {
            media.MediaPlayer.Play();
            IsPlay = true;
        }
    }
    bool isopen = false;
    private void ToggleButton_Click(object sender, RoutedEventArgs e)
    {
        if (isopen)
        {
            Close.Begin();
            isopen = !isopen;
            return;
        }
        Show.Begin();
        isopen = !isopen;
        return;
    }

}
