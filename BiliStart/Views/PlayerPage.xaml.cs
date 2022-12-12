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

namespace BiliStart.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
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




    



    bool IsPlay = false;

    private async void PlayerPage_Loaded(object sender, RoutedEventArgs e)
    {
       
        this.media.SetMediaPlayer(ViewModel.NowMediaPlayer);
        //在这里订阅一个媒体加载完毕事件
        ViewModel.NowMediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
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

    public object OldContent
    {
        get;set;    
    }

    bool IsFull;
    object oldtitbar;


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
            IsFull = false;
            App.MainWindow.SetTitleBar(TitleBar);
            MoreColumn.Width = new GridLength(450, GridUnitType.Pixel);
            bottommenu.Visibility = Visibility.Visible;
            mediaborder.CornerRadius = new CornerRadius(10);
            ViewModel.FullChanged(false);
            App.MainWindow.ExtendsContentIntoTitleBar = true;
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
            IsFull = true;
            mediaborder.CornerRadius = new CornerRadius(0);
            ViewModel.FullChanged(true);
            App.MainWindow.ExtendsContentIntoTitleBar = false;
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



    private void process_ManipulationCompleted(object sender, Microsoft.UI.Xaml.Input.ManipulationCompletedRoutedEventArgs e)
    {
        media.MediaPlayer.PlaybackSession.Position = TimeSpan.FromMilliseconds((sender as Slider)!.Value);
    }

   
}
