using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ItemsViewModel;

[INotifyPropertyChanged]
public partial class HomeItemViewModel
{
    private BiliBiliAPI.Models.HomeVideo.HomeDataItem Item;

    public BiliBiliAPI.Models.HomeVideo.HomeDataItem _Item
    {
        get => Item;
        set=>SetProperty(ref Item, value);
    }

}
