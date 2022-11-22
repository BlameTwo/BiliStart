using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.TopList;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ItemsViewModel;

[ObservableObject]
public partial class MusicRankItemViewModel
{
    private MusicRankData Data;

    public MusicRankData _Data
    {
        get => Data;
        set=>SetProperty(ref Data, value);
    }

}
