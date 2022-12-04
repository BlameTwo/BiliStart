// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using BiliBiliAPI.Video;
using BiliBiliAPI.Models;
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
using Microsoft.Graphics.Canvas.UI.Xaml;
using BiliBiliAPI.Models.HomeVideo;
using System.Collections.ObjectModel;
using Windows.Media.Core;
using FFmpegInteropX;
using BiliStart.Contracts.Services;
using BiliStart.Services;

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
        if (NowMediaPlayer != null)
        {
            NowMediaPlayer.MediaOpened -= MediaPlayer_MediaOpened;

            NowMediaPlayer.Pause();
            NowMediaPlayer.Dispose();
        }
        Timer.Tick -= Timer_Tick;
        Source = null;
        NowMediaPlayer = null;

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
            switch (playerArgs.Type)
            {
                case GoToType.Video:
                    ViewModel.InitVideo(playerArgs);
                    break;
                case GoToType.Animation:
                    break;
                case GoToType.Movie:
                    break;
            }
        }
    }




    public async void Support_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // https://www.microsoft.com/en-us/p/hevc-video-extensions-from-device-manufacturer/9n4wgh0z6vhq
        // HEVC解码器位置
        //https://apps.microsoft.com/store/detail/av1-video-extension/9MVZQVXJBQ9V?hl=zh-cn&gl=cn
        //AV1解码器位置
        if(e.AddedItems.Count > 0)
        {
            if (e.AddedItems[0] != null)
            {
                Support_Formats value = e.AddedItems[0] as Support_Formats;
                foreach (var item in ViewModel.VI.Dash.DashVideos)
                {
                    if (item.ID == value!.Quality)
                    {
                        // hev 和 avc
                        if (item.Codecs.StartsWith("avc"))
                        {
                            Source = await PlayerHelper.CreateMediaSourceAsync(item, ViewModel.VI.Dash.DashAudio[0]);
                            NowMediaPlayer.SetMediaSource(Source.AdaptiveMediaSource);
                            break;
                        }
                    }
                }
            }
        }
    }


    public MediaPlayer NowMediaPlayer = new();

    public MediaSource? Source;

    bool IsPlay = false;

    private async void PlayerPage_Loaded(object sender, RoutedEventArgs e)
    {
       
        this.media.SetMediaPlayer(NowMediaPlayer);
        NowMediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
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
            Grid.SetRowSpan(media,1);
            Grid.SetRow(media, 1);
            IsFull = false;
            App.MainWindow.SetTitleBar(TitleBar);
            ViewModel.FullChanged(false);
            App.MainWindow.ExtendsContentIntoTitleBar = true;
        }
        else
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hwnd));
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            App.MainWindow.SetTitleBar(null);
            Grid.SetRowSpan(media, 3);
            Grid.SetRow(media, 0);
            IsFull = true;
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
