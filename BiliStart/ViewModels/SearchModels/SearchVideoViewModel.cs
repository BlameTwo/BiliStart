using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Search;
using BiliBiliAPI.Search;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;

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
    }



    public ITipShow TipShow
    {
        get;
    }

    private async void OnSearchChanged(string value)
    {
        var result = await Search.GetVideo(value, 1, BiliBiliAPI.Models.Search.OrderBy.Default,0);
        if(result.Data != null)
        {
            ItemData = result.Data.Items.ToObservableCollection();
        }
    }

    private ObservableCollection<BiliBiliAPI.Models.Search.Item> Items;

    public ObservableCollection<BiliBiliAPI.Models.Search.Item> ItemData
    {
        get => Items;
        set => SetProperty(ref Items,value);
    }

}
