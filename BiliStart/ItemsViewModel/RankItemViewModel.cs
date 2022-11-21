﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ItemsViewModel;


[INotifyPropertyChanged]
public partial class RankItemViewModel
{
    public RankItemViewModel()
    {

    }


    private BiliBiliAPI.Models.TopList.TopVideo Video;

    public BiliBiliAPI.Models.TopList.TopVideo _Video
    {
        get => Video;
        set=>SetProperty(ref Video, value);
    }

}
