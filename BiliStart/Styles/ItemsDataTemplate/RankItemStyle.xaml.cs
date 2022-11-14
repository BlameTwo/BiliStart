// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BiliStart.Styles.ItemsDataTemplate;
public sealed partial class RankItemStyle : UserControl
{
    public RankItemStyle()
    {
        this.InitializeComponent();
    }

    public RankItemViewModel ViewModel
    {
        get;set;
    }


    public BiliBiliAPI.Models.TopList.TopVideo Data
    {
        get => (BiliBiliAPI.Models.TopList.TopVideo)GetValue(DataProperty);
        set =>SetValue(DataProperty, value);
    }

    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(BiliBiliAPI.Models.TopList.TopVideo), typeof(RankItemStyle), new PropertyMetadata(default(BiliBiliAPI.Models.TopList.TopVideo),
            new PropertyChangedCallback((s, e) =>
            {
                (s as RankItemStyle)!.ViewModel = new RankItemViewModel() { _Video = (BiliBiliAPI.Models.TopList.TopVideo)e.NewValue };
            })));


}
