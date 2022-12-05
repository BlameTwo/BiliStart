using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI;
using BiliBiliAPI.Models.Account.Dynamic;
using BiliBiliAPI.Models.User;
using BiliBiliAPI.User;
using BiliStart.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualStudio.Services.Profile;

namespace BiliStart.ViewModels;
public partial class DynamicViewModel:ObservableRecipient
{
    public DynamicViewModel()
    {
        _UpdateItems = new();
        LiveItems= new();
        IsActive = true;
    }
    BiliBiliAPI.Account.Dynamic.MyDynamic Dynamic = new();
    Users Users = new();
    [RelayCommand]
    public async Task Loaded()
    {
        _UpdateItems.Clear();
        LiveItems.Clear();
        var livevalues = (await Dynamic.GetDynamicUp_UpDateList()).Data;
        MyInfoData = livevalues.MyInfo;
        _MyData = (await Users.GetUser(BiliBiliArgs.TokenSESSDATA.Mid)).Data;
        if (livevalues.LiveInfo != null)
        {
            foreach (var item in livevalues.LiveInfo.Items.ToObservableCollection())
            {
                LiveItems.Add(ToDynamicLive_My_VM<Dynamic_Live_Items>(item));
            }
        }
        if(livevalues.UpList != null)
        {
            foreach (var item in livevalues.UpList.ToObservableCollection())
            {
                _UpdateItems.Add(ToDynamicLive_My_VM<Dynamic_Live_Items>(item));
            }
        }
    }

    private UpData MyData;

    public UpData _MyData
    {
        get => MyData;
        set => SetProperty(ref MyData, value);
    }


    private Dynamic_MyInfo _MyInfoData;

    public Dynamic_MyInfo MyInfoData
    {
        get => _MyInfoData;
        set => SetProperty(ref _MyInfoData, value);
    }

    private string Topimage;

    public string _TipImage
    {
        get => Topimage;
        set => SetProperty(ref Topimage, value);
    }


    public DynamicLive_My_VM ToDynamicLive_My_VM<T>(T value)
        where T : UpListItem
    {
        DynamicLive_My_VM dynamicLive_My_VM = new()
        {
            IsUpDate=value.IsUpDate,
            Cover = value.Cover,
            IsReserve= value.IsReserve,
            Mid = value.Mid,
            Title= value.Title, 
            UpName = value.UpName, 
          
        };
        if(value is Dynamic_Live_Items value2)
        {
            dynamicLive_My_VM.Url = value2.Url;
        }
        return dynamicLive_My_VM;
    }

    private ObservableCollection<DynamicLive_My_VM> liveitems;

    public ObservableCollection<DynamicLive_My_VM> LiveItems
    {
        get => liveitems;
        set=>SetProperty(ref liveitems, value); 
    }


    private ObservableCollection<DynamicLive_My_VM> updateitems;

    public ObservableCollection<DynamicLive_My_VM> _UpdateItems
    {
        get => updateitems;
        set => SetProperty(ref updateitems, value);
    }
}
