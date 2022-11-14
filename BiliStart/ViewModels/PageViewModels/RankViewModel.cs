using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.TopVideos;
using BiliBiliAPI.Video;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels.PageViewModels;
public partial class RankViewModel:ObservableRecipient
{
    TopListVideo Video = new TopListVideo();
    public RankViewModel(ITipShow tipshow)
    {
        Tipshow = tipshow;
    }

    [RelayCommand]
    public async Task Loaded()
    {
        var result = await Video.GetTopVideo(cid:BiliBiliAPI.Cid.All,3);
        _Items = result.Data.List.ToObservableCollection();
        Tipshow.SendMessage(null,result.Data.Note);
    }

    [ObservableProperty]
    private string? note;

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
}
