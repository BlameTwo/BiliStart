using System.Collections.ObjectModel;
using BiliBiliAPI.Account.Dynamic;
using BiliBiliAPI.Models.Account.Dynamic;
using BiliStart.ItemsViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels.PageViewModels.DynamicsViewModels;

public partial class MyInfoViewModel:ScrolViewModelBase
{
    MyDynamic myDynamic = new();
    public MyInfoViewModel()
    {
        Items = new();
        AddData = new AsyncRelayCommand(async () => await Refersh());
    }

    [RelayCommand]
    public async Task Loaded()
    {
        await Refersh();
    }
    string Offset="";
    int index = 1;
    private async Task Refersh()
    {
        var result = await myDynamic.GetDynamic(MyDynamic.DynamicEnum.All, Offset,index);
        Offset = result.Data.OffSet;
        foreach (var item in result.Data.DynamicList.ToObservableCollection())
        {
            Items.Add(new DefaultDynamicViewModel() { Basic = item.Basic,
            ID = item.ID, DynamicType = item.DynamicType, IsVisible = item.IsVisible, Modules = item.Modules});
        }
        index++;
    }


    private ObservableCollection<DefaultDynamicViewModel> Items;

    public ObservableCollection<DefaultDynamicViewModel> _Items
    {
        get => Items;
        set => SetProperty(ref Items, value);
    }

}
