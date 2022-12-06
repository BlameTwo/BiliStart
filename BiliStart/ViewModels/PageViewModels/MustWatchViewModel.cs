using System.Collections.ObjectModel;
using BiliBiliAPI.Models.HomeVideo;
using BiliBiliAPI.Models.TopList;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.ViewModels.PageViewModels;
public partial class MustWatchViewModel:ObservableRecipient
{
    BiliBiliAPI.TopLists.MustWatch MustWatch = new BiliBiliAPI.TopLists.MustWatch();
    BiliBiliAPI.Video.Video Video = new BiliBiliAPI.Video.Video();
    [RelayCommand]
    public async  Task Loaded()
    {
        var result = await MustWatch.GetVideos();
        SubTitle = result.Data.SubTitle;
        Items = result.Data.List.ToObservableCollection();

    }

    public MustWatchViewModel(IGoVideo goVideo)
    {
        GoVideo = goVideo;
    }


    private string subtitle;

    public string SubTitle
    {
        get => subtitle;
        set => SetProperty(ref subtitle,value);
    }

    public async void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count < 0) return;
        var value = e.AddedItems[0] as MustWatchDataItem;
        BiliStart.ViewModels.Models.PlayerArgs arg = new()
        {
            Aid = long.Parse(value.Aid),
            Bvid = value.Bvid,
            Content = (await Video.GetVideosContent(value.Aid, BiliBiliAPI.Models.VideoIDType.AV)).Data
        };
        GoVideo.PlayerArgs= arg;
        GoVideo.Go();
    }

    private ObservableCollection<MustWatchDataItem> item;

    public ObservableCollection<MustWatchDataItem> Items
    {
        get => item;
        set => SetProperty(ref item, value);

    }
    public IGoVideo GoVideo
    {
        get;
    }
}
