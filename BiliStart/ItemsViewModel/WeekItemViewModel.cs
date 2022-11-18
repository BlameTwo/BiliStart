using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.TopList;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ItemsViewModel;
public class WeekItemViewModel : ObservableObject
{
    public WeekItemViewModel()
    {

    }

    private WeekItemData Data;

    public WeekItemData _Data
    {
        get => Data;
        set => SetProperty(ref Data, value);
    }

}
