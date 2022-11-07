using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.TopList;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ViewModel.HotViewModel;
public class HotWeakDTVM:ObservableRecipient
{
    public HotWeakDTVM()
    {

    }




    private WeekItemData Item;

    public WeekItemData _Items
    {
        get => Item;
        set => SetProperty(ref Item, value);
    }



}
