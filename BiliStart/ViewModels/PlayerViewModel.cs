using BiliBiliAPI.Models.Videos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace BiliStart.ViewModels;
public partial class PlayerViewModel:ObservableRecipient
{
    public PlayerViewModel()
    {
        _FullButtonText = "\uE740";

        Timer.Tick += Timer_Tick;
        Timer.Start();
    }

    private void Timer_Tick(object? sender, object e)
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            SliderValue = NowMediaPlayer.PlaybackSession.Position.TotalMilliseconds;
        });

    }


    private VideoInfo VideoInfo;

    public VideoInfo _VIdeoInfo
    {
        get => VideoInfo;
        set => SetProperty(ref VideoInfo, value);
    }




    private double _MaxValue;

    public double MaxValue
    {
        get => _MaxValue;
        set=>SetProperty(ref _MaxValue, value);
    }

    public void MediaClear()
    {
        NowMediaPlayer.Pause();
        Timer.Stop();
        Timer.Tick-= Timer_Tick;
        Source = null;
        NowMediaPlayer.Dispose();
        NowMediaPlayer = null;
        
    }

    private double _SliderValue;

    public double SliderValue
    {
        get => _SliderValue;
        set => SetProperty(ref _SliderValue, value);
    }


    DispatcherTimer Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

    public MediaPlayer NowMediaPlayer;

    public MediaSource? Source;

    private string FullButtonText;

    public string _FullButtonText
    {
        get => FullButtonText;
        set => SetProperty(ref FullButtonText, value);
    }

    public void FullChanged(bool isfull)
    {
        switch (isfull)
        {
            case true:
                _FullButtonText = "\uE73F";
                break;
            case false:
                _FullButtonText = "\uE740";
                break;
        }
    }

    private VideosContent Content;

    public VideosContent _Content
    {
        get => Content;
        set=>SetProperty(ref Content, value);
    }

    [RelayCommand]
    public void GoBack()
    {
        (App.MainWindow.Content as MainPage)!.RootFrame.GoBack();
    }
}
