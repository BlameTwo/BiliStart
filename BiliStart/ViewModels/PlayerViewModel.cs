using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Videos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace BiliStart.ViewModels;
public partial class PlayerViewModel:ObservableRecipient
{
    public PlayerViewModel()
    {
        _FullButtonText = "\uE740";
    }

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
