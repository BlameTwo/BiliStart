using System.Collections.ObjectModel;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using Newtonsoft.Json.Linq;

namespace BiliStart.ViewModels.SearchModels;


public partial class SearchVideoViewModel:SearchViewModelBase
{

    public SearchVideoViewModel(ITipShow tipShow)
    {
        TipShow = tipShow;

        Changed = new Action<string>((str) =>
        {
            OnSearchChanged(value:str);
        });

        Changing = new Action<string>((str) =>
        {
            //搜索目标改变过程中………………
        });

        AddData = new AsyncRelayCommand(async() => await adddata());
    }
    int Index = 1;
    private async Task adddata()
    {
        var result = await Search.GetVideo(this._SearchKey, Index, BiliBiliAPI.Models.Search.OrderBy.Default, 0);
        if (result.Data != null)
        {
            foreach (var item in result.Data.Items.ToObservableCollection())
            {
                //过滤掉电影和番剧
                if (item.Goto == "av")
                {
                    ItemData.Add(item);
                }
            }
        }
        Index++;
    }

    public ITipShow TipShow
    {
        get;
    }

    private async void OnSearchChanged(string value)
    {
        if (ItemData == null) ItemData =new();
        var result = await Search.GetVideo(value, Index, BiliBiliAPI.Models.Search.OrderBy.Default,0);
        if(result.Data != null)
        {
            foreach (var item in result.Data.Items.ToObservableCollection())
            {
                //过滤掉电影和番剧
                if (item.Goto == "av")
                {
                    ItemData.Add(item);
                }
            }
        }
        Index++;
    }

    private ObservableCollection<BiliBiliAPI.Models.Search.Item> Items;

    public ObservableCollection<BiliBiliAPI.Models.Search.Item> ItemData
    {
        get => Items;
        set => SetProperty(ref Items,value);
    }


    BiliBiliAPI.Video.Video Video = new();


    [RelayCommand]
    public async Task GoVideo(BiliBiliAPI.Models.Search.Item item)
    {
        BiliStart.ViewModels.Models.PlayerArgs arg2 = new BiliStart.ViewModels.Models.PlayerArgs()
        {
            Aid = long.Parse(item.LinkParam),
        };
        var result = (await Video.GetVideosContent(item.LinkParam, BiliBiliAPI.Models.VideoIDType.AV)).Data;
        arg2.Content = result;
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            //注入导航
            var navigationService = App.GetService<IAppNavigationService>();
            navigationService.NavigationTo(AppNavigationViewsEnum.RootFrame, typeof(PlayerViewModel).FullName!, arg2);
        });
    }

}
