using System.Collections.ObjectModel;
using BiliBiliAPI.Models;
using BiliBiliAPI.Models.TopList;
using BiliStart.Contracts.Services;
using BiliStart.Services;
using BiliStart.ViewModels.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.ViewModels.PageViewModels;
public partial class WeekViewModel:ObservableRecipient
{
    BiliBiliAPI.TopLists.EveryoneWeek Week = new();

    public WeekViewModel(IGoVideo goVideo)
    {
        IsActive= true;
        Loaded = new RelayCommand(loaded);
        GoVideo = goVideo;
    }

    private async void loaded()
    {
        HeaderList = await Week.GetWeekList();
        if(HeaderList.Data != null)
        {
            _WeekHeaderItem = HeaderList.Data.List.ToObservableCollection();
        }
    }

    ResultCode<EveryoneList> HeaderList
    {
        get;set;    
    }

    

    private string title;
    public string Title
    {
        get => title;
        set=>SetProperty(ref title, value);
    }

    BiliBiliAPI.Video.Video Video = new();

    public async void AdaptiveGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count>0)
        {
            var value = e.AddedItems[0] as BiliBiliAPI.Models.TopList.WeekItemData;
            var arg = new PlayerArgs()
            {
                Aid = long.Parse(value.Aid),
                Bvid = value.Bvid,
                Content = ((await Video.GetVideosContent(value.Aid, BiliBiliAPI.Models.VideoIDType.AV)).Data)
            };
            GoVideo.PlayerArgs = arg;
            GoVideo.Go();
        }
    }


    private ObservableCollection<EveryoneWeekData> WeekHeaderItem;

    public ObservableCollection<EveryoneWeekData> _WeekHeaderItem
    {
        get => WeekHeaderItem;
        set=>SetProperty(ref WeekHeaderItem, value);
    }

    WeekItem ?NowSelection;

    [RelayCommand]
    public async Task Selection(EveryoneWeekData data)
    {
        var list = await Week.GetWeekTopList(data.Number);
        NowSelection = list.Data;
        _Items = list.Data.Items.ToObservableCollection();
        Title = $"“{list.Data.Config.SubJect}";
    }

    private ObservableCollection<WeekItemData> Items;

    public ObservableCollection<WeekItemData> _Items
    {
        get => Items;
        set => SetProperty(ref Items, value);
    }


    public RelayCommand Loaded
    {
        get;private set;
    }
    public IGoVideo GoVideo
    {
        get;
    }
}
