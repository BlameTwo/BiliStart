// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using BiliBiliAPI.Video;
using BiliBiliAPI.Models;
using BiliBiliAPI.Models.Videos;
using BiliStart.Contracts.Services;
using BiliStart.ViewModels;
using BiliStart.ViewModels.Models;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PInvoke;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.ViewManagement;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using WinRT;
using Windows.Media.Playback;
using System.Runtime.InteropServices;
using BiliStart.Helpers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BiliStart.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PlayerPage : Microsoft.UI.Xaml.Controls.Page
{
    DispatcherTimer Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1)};

    public PlayerViewModel ViewModel
    {
        get;
    }

    public PlayerPage()
    {
        ViewModel = App.GetService<PlayerViewModel>();
        this.InitializeComponent();
        Loaded += PlayerPage_Loaded;
        IsFull = false;
        Timer.Tick += Timer_Tick;
    }

    private void Timer_Tick(object? sender, object e)
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {

        });
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        Loaded -= PlayerPage_Loaded;
        media.MediaPlayer.MediaOpened -= MediaPlayer_MediaOpened;
        media.MediaPlayer.Pause();
        media.MediaPlayer.Dispose();
        ViewModel.NowMediaPlayer.Dispose();
        GC.Collect();
        GC.WaitForPendingFinalizers(); base.OnNavigatingFrom(e);
        ViewModel.Source = null;
        media.Source = null;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        System.GC.Collect();
    }


    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        VideoInfo info = (await Video.GetVideo((e.Parameter as ResultCode<VideosContent>)!.Data, BiliBiliAPI.Models.VideoIDType.AV)).Data;
        ViewModel.Source = await PlayerHelper.CreateMediaSourceAsync(info.Dash.DashVideos[0], info.Dash.DashAudio[0]);
        ViewModel.NowMediaPlayer = new MediaPlayer();
        ViewModel.NowMediaPlayer.SetMediaSource(ViewModel.Source.AdaptiveMediaSource);
        media.SetMediaPlayer(ViewModel.NowMediaPlayer);
        media.MediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
    }



    Video Video = new Video();


    private async void PlayerPage_Loaded(object sender, RoutedEventArgs e)
    {
        
    }

    private void MediaPlayer_MediaOpened(Windows.Media.Playback.MediaPlayer sender, object args)
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            media.MediaPlayer.Play();
        });
    }


    public PlayerArgs PlayArgs
    {
        get=> (PlayerArgs)GetValue(PlayArgsProperty);
        set => SetValue(PlayArgsProperty, value);
    }

    // Using a DependencyProperty as the backing store for PlayArgs.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PlayArgsProperty =
        DependencyProperty.Register("PlayArgs", typeof(PlayerArgs), typeof(PlayerPage), new PropertyMetadata(default(PlayerArgs)));

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
}
