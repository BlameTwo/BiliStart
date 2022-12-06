using System.Collections.ObjectModel;
using BiliBiliAPI.Models.Account.Dynamic;
using BiliBiliAPI.User;
using BiliStart.Contracts.Services;
using BiliStart.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels;
public partial class DynamicViewModel:ObservableRecipient
{
    
    public DynamicViewModel(IAppNavigationService navigationService, IPageService pageService, IAppNavigationViewService navigationViewService)
    {
        IsActive = true;
        NavigationService = navigationService;
        PageService = pageService;
        NavigationViewService = navigationViewService;
    }
    BiliBiliAPI.Account.Dynamic.MyDynamic Dynamic = new();
    Users Users = new();


    [RelayCommand]
    public async Task Loaded()
    {
        var livevalues = (await Dynamic.GetDynamicUp_UpDateList()).Data;
        if (livevalues.LiveInfo != null)
        {
            LiveItems = new();
            foreach (var item in livevalues.LiveInfo.Items.ToObservableCollection())
            {
                LiveItems.Add(ToDynamicLive_My_VM<Dynamic_Live_Items>(item));
            }
        }
        if (livevalues.UpList != null)
        {
            _UpdateItems = new();
            foreach (var item in livevalues.UpList.ToObservableCollection())
            {
                _UpdateItems.Add(ToDynamicLive_My_VM<Dynamic_Live_Items>(item));
            }
        }
    }

    


    public DynamicLive_My_VM ToDynamicLive_My_VM<T>(T value)
        where T : UpListItem
    {
        DynamicLive_My_VM dynamicLive_My_VM = new()
        {
            IsUpDate = value.IsUpDate,
            Cover = value.Cover,
            IsReserve = value.IsReserve,
            Mid = value.Mid,
            Title = value.Title,
            UpName = value.UpName,

        };
        if (value is Dynamic_Live_Items value2)
        {
            dynamicLive_My_VM.Url = value2.Url;
        }
        return dynamicLive_My_VM;
    }

    private ObservableCollection<DynamicLive_My_VM> liveitems;

    public ObservableCollection<DynamicLive_My_VM> LiveItems
    {
        get => liveitems;
        set => SetProperty(ref liveitems, value);
    }


    private ObservableCollection<DynamicLive_My_VM> updateitems;

    public ObservableCollection<DynamicLive_My_VM> _UpdateItems
    {
        get => updateitems;
        set => SetProperty(ref updateitems, value);
    }

    public IAppNavigationService NavigationService
    {
        get;
    }
    public IPageService PageService
    {
        get;
    }
    public IAppNavigationViewService NavigationViewService
    {
        get;
    }
}
