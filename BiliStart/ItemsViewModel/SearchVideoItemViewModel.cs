using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ItemsViewModel;

[ObservableObject]
public partial class SearchVideoItemViewModel
{
    public SearchVideoItemViewModel()
    {

    }

    private BiliBiliAPI.Models.Search.Item Data;

    public BiliBiliAPI.Models.Search.Item _Data
    {
        get => Data;
        set => SetProperty(ref Data, value);
    }

}
