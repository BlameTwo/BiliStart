using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ItemsViewModel;

public partial class HotItemViewModel:ObservableObject
{
    private BiliBiliAPI.Models.HomeVideo.Item Data;

    public BiliBiliAPI.Models.HomeVideo.Item _Data
    {
        get => Data;
        set => SetProperty(ref Data, value);
    }

}
