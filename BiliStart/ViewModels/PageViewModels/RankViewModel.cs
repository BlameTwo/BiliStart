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
using PInvoke;

namespace BiliStart.ViewModels.PageViewModels;
public partial class RankViewModel : ObservableRecipient
{
    TopListVideo Video = new TopListVideo();
    public RankViewModel(ITipShow tipshow)
    {
        Tipshow = tipshow;
    }

    public async Task Loaded()
    {
        var result = await Video.GetTopVideo(cid: BiliBiliAPI.Cid.All, 3);
        _Items = result.Data.List.ToObservableCollection();
        Tipshow.SendMessage(null, result.Data.Note);
        Title = "排行榜";
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

    private string title;

    public string Title
    {
        get => title;
        set=>SetProperty(ref title,value);
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
        }
        catch (Exception)
        {
            Title = "分区错误";
            Tipshow.SendMessage(null,"当前分区无视频排行，请切换其他分区");
        }
        
    }
}
