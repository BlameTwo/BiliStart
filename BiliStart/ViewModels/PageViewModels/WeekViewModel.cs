using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models;
using BiliBiliAPI.Models.TopList;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels.PageViewModels;
public partial class WeekViewModel:ObservableRecipient
{
    BiliBiliAPI.TopLists.EveryoneWeek Week = new();

    public WeekViewModel()
    {
        IsActive= true;
        Loaded = new RelayCommand(loaded);
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
}
