using System.Collections.ObjectModel;
using BiliBiliAPI.Models.TopList;
using BiliBiliAPI.TopVideos;
using BiliStart.Contracts.Services;
using BiliStart.ViewModels.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.ViewModels.PageViewModels;
public partial class RankViewModel : ObservableRecipient
{
    TopListVideo Video = new TopListVideo();
    public RankViewModel(ITipShow tipshow,IGoVideo goVideo)
    {
        _NullPopup =  Visibility.Collapsed;
        Tipshow = tipshow;
        GoVideo = goVideo;
    }

    BiliBiliAPI.Video.Video VContent = new();

    public async Task Loaded()
    {
        var result = await Video.GetTopVideo(cid: BiliBiliAPI.Cid.All, 3);
        _Items = result.Data.List.ToObservableCollection();
        Tipshow.SendMessage(null, result.Data.Note);
        Title = "排行榜";
        _NullPopup = Visibility.Collapsed;
        
    }



    public async void AdaptiveGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var value = e.AddedItems[0] as BiliBiliAPI.Models.TopList.TopVideo;
        var arg = new PlayerArgs()
        {
            Aid = long.Parse(value.Aid),
            Bvid = value.Bvid
            , Content = ((await VContent.GetVideosContent(value.Aid, BiliBiliAPI.Models.VideoIDType.AV)).Data)
        };
        GoVideo.PlayerArgs = arg;
        GoVideo.Go();
    }

    private ObservableCollection<BiliBiliAPI.Models.TopList.TopVideo> Items;

    public ObservableCollection<BiliBiliAPI.Models.TopList.TopVideo> _Items
    {
        get => Items;
        set => SetProperty(ref Items, value);
    }
    public ITipShow Tipshow
    {
        get;
    }
    public IGoVideo GoVideo
    {
        get;
    }

    private string title;

    public string Title
    {
        get => title;
        set=>SetProperty(ref title,value);
    }

    private Visibility NullPopup;

    public Visibility _NullPopup
    {
        get => NullPopup;
        set => SetProperty(ref NullPopup, value);
    }

    private string TipMessage;

    public string _TipMessage
    {
        get => TipMessage;
        set => SetProperty(ref TipMessage, value);
    }


    public async Task refersh(string value)
    {
        try
        {
            var valu2 =  int.Parse(value);
            var result = await Video.GetTopVideo(cid: valu2, 3);
            _Items = result.Data.List.ToObservableCollection();
            Tipshow.SendMessage(null, result.Data.Note);
            Title = "分区排行";
            _NullPopup = Visibility.Collapsed;
        }
        catch (Exception)
        {
            Title = "分区错误";
            Tipshow.SendMessage(null, "当前分区无视频排行，请切换其他分区");
            _NullPopup = Visibility.Visible;
            _TipMessage = "所选分区无内容";
        }
        
    }


}
