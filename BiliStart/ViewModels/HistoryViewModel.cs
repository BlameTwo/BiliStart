using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Account;
using BiliStart.ItemsViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels;

public partial class HistoryViewModel:ScrolViewModelBase
{
    BiliBiliAPI.Account.History.History GetHistory = new();

    [ObservableProperty]
    ObservableCollection<HistoryTabs> _Tabs;

    [ObservableProperty]
    ObservableCollection<HistoryItemViewModel> _Items;

    public HistoryViewModel()
    {
        IsActive = true;
        AddData = new AsyncRelayCommand(async () => await addData());
        Items = new();
    }

    private async Task addData()
    {
        var result = await GetHistory.GetHistory(NowMax, 25, selecttype);
        if(result.Data != null)
        {
            NowMax = result.Data.Cursor;
            foreach (var item in result.Data.Items)
            {
                Items.Add(item.ClassConvert<HisToryDataItem, HistoryItemViewModel>());
            }
        }
    }
    GetHistoryType selecttype = GetHistoryType.AV;
    Cursor NowMax = new();
    [RelayCommand]
    async Task TabsSelected(HistoryTabs tab)
    {//archive live article
        switch (tab.Type)
        {
            case "archive":
                selecttype = GetHistoryType.AV;
                break;
            case "live":
                selecttype = GetHistoryType.Live;
                break;
            case "article":
                selecttype = GetHistoryType.Article;
                break;
        }
        var list = await GetHistory.GetHistory(new Cursor(), 25, selecttype);
        if(list.Data != null)
        {
            NowMax = list.Data.Cursor;
            Items.Clear();
            NowMax = list.Data.Cursor;
            foreach (var item in list.Data.Items.ToObservableCollection())
            {
                Items.Add(item.ClassConvert<HisToryDataItem, HistoryItemViewModel>());
            }
        }
    }

}
