using System.Collections.ObjectModel;
using BiliBiliAPI.Models.TopList;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels.PageViewModels;
public partial class MustWatchViewModel:ObservableRecipient
{
    BiliBiliAPI.TopLists.MustWatch MustWatch = new BiliBiliAPI.TopLists.MustWatch();

    [RelayCommand]
    public async  Task Loaded()
    {
        var result = await MustWatch.GetVideos();
        SubTitle = result.Data.SubTitle;
        Items = result.Data.List.ToObservableCollection();

    }




    private string subtitle;

    public string SubTitle
    {
        get => subtitle;
        set => SetProperty(ref subtitle,value);
    }



    private ObservableCollection<MustWatchDataItem> item;

    public ObservableCollection<MustWatchDataItem> Items
    {
        get => item;
        set => SetProperty(ref item, value);

    }

}
