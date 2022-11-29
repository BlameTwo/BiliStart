using System.Collections.ObjectModel;
using BiliBiliAPI.Models;
using BiliBiliAPI.Models.Videos;
using BiliStart.Helpers;
using BiliStart.ViewModels.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace BiliStart.ViewModels;
public partial class PlayerViewModel:ObservableRecipient
{
    public PlayerViewModel()
    {
        _FullButtonText = "\uE740";

    }

    public PlayerArgs Args
    {
        get;set;    
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

    

    private double _SliderValue;

    public double SliderValue
    {
        get => _SliderValue;
        set => SetProperty(ref _SliderValue, value);
    }




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

    private ObservableCollection<Support_Formats> Supports;

    public ObservableCollection<Support_Formats> _Supports
    {
        get => Supports;
        set => SetProperty(ref Supports, value);
    }

    readonly BiliBiliAPI.Video.Video Video = new();

    public VideoInfo VI = null;

    public async void InitVideo(ViewModels.Models.PlayerArgs playerArgs)
    {
        VI = (await Video.GetVideo(playerArgs.Content, VideoIDType.AV)).Data;
        _Supports = VI.Support_Formats.ToObservableCollection();
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
