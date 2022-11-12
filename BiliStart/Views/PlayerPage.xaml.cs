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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

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
        this.InitializeComponent();
        Loaded += PlayerPage_Loaded;
    }

    protected async override void OnNavigatedFrom(NavigationEventArgs e)
    {
        media.MediaPlayer.Pause();
        media.MediaPlayer.Dispose();
    }


    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        VideoInfo info = (await Video.GetVideo((e.Parameter as ResultCode<VideosContent>)!.Data, BiliBiliAPI.Models.VideoIDType.AV)).Data;

        media.Source =(Windows.Media.Playback.IMediaPlaybackSource) await CreateMediaSourceAsync(info.Dash.DashVideos[0], info.Dash.DashAudio[0]);


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

    public async Task<MediaSource> CreateMediaSourceAsync(BiliBiliAPI.Models.Videos.DashVideo Video, BiliBiliAPI.Models.Videos.DashVideo Audio)
    {
        try
        {
            Windows.Web.Http.HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Referer = new Uri("https://www.bilibili.com");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            var mpdStr = $@"<MPD xmlns=""urn:mpeg:DASH:schema:MPD:2011""  profiles=""urn:mpeg:dash:profile:isoff-on-demand:2011"" type=""static"">
                                  <Period  start=""PT0S"">
                                    <AdaptationSet>
                                      <ContentComponent contentType=""video"" id=""1"" />
                                      <Representation bandwidth=""{Video.BandWidth}"" codecs=""{Video.Codecs}"" height=""{Video.Height}"" id=""{Video.ID}"" mimeType=""{Video.VideoType}"" width=""{Video.Width}"">
                                        <BaseURL></BaseURL>
                                        <SegmentBase indexRange=""{Video.SegmentBase.indexRange}"">
                                          <Initialization range=""{Video.SegmentBase.Initialization}"" />
                                        </SegmentBase>
                                      </Representation>
                                    </AdaptationSet>
                                    {{audio}}
                                  </Period>
                                </MPD>
                                ";
            if (Audio == null)
                mpdStr = mpdStr.Replace("{audio}", "");
            else
                mpdStr = mpdStr.Replace("{audio}", $@"<AdaptationSet>
                                      <ContentComponent contentType=""audio"" id=""2"" />
                                      <Representation bandwidth=""{Audio.BandWidth}"" codecs=""{Audio.Codecs}"" id=""{Audio.ID}"" mimeType=""{Audio.VideoType}"" >
                                        <BaseURL></BaseURL>
                                        <SegmentBase indexRange=""{Audio.SegmentBase.indexRange}"">
                                          <Initialization range=""{Audio.SegmentBase.Initialization}"" />
                                        </SegmentBase>
                                      </Representation>
                                    </AdaptationSet>");
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(mpdStr)).AsInputStream();
            var soure = await AdaptiveMediaSource.CreateFromStreamAsync(stream, new Uri(Video.BackupUrl[0]), "application/dash+xml", httpClient);
            var s = soure.Status;
            soure.MediaSource.DownloadRequested += (sender, args) =>
            {
                if (args.ResourceContentType == "audio/mp4" && Audio != null)
                {
                    args.Result.ResourceUri = new Uri(Audio.Base_Url);
                }
            };
            return MediaSource.CreateFromAdaptiveMediaSource(soure.MediaSource);
        }
        catch (Exception)
        {
            return null;
        }

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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
        var appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hwnd));
        appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
    }

}
