using System.Collections.ObjectModel;
using BiliBiliAPI.Models;
using BiliBiliAPI.Models.Videos;
using BiliStart.Contracts.Services;
using BiliStart.Helpers;
using BiliStart.Services;
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
    public PlayerViewModel(ILocalSettingsService localSettingsService)
    {
        _FullButtonText = "\uE740";
        LocalSettingsService = localSettingsService;
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
        switch (await LocalSettingsService.ReadSettingAsync<int>(BiliStart.Models.Settings.Player_Supper_Supper))
        {
            default:
            case 0:
                _SupportIndex = GetSupportIndex("4K");
                break;
            case 1:
                _SupportIndex = GetSupportIndex("1080");
                break;
            case 2:
                _SupportIndex = GetSupportIndex("720");
                break;
        }
    }


    int GetSupportIndex(string value)
    {
        for (int i = 0; i < _Supports.Count; i++)
        {
            //找到
            if (_Supports[i].New_description.IndexOf(value) != -1)
            {
                return i;
            }
        }
        return -1;
    }

    private int SupportIndex;

    public int _SupportIndex
    {
        get =>SupportIndex;
        set => SetProperty(ref SupportIndex, value);
    }


    private VideosContent Content;

    public VideosContent _Content
    {
        get => Content;
        set=>SetProperty(ref Content, value);
    }
    public ILocalSettingsService LocalSettingsService
    {
        get;
    }

    [RelayCommand]
    public void GoBack()
    {
        (App.MainWindow.Content as MainPage)!.RootFrame.GoBack();
    }
}
