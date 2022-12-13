// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
public sealed partial class HotItemStyle : UserControl
{
    public HotItemStyle()
    {
        this.InitializeComponent();
    }


    public BiliStart.ItemsViewModel.HotItemViewModel ViewModel
    {
        get;set;
    }



    public BiliBiliAPI.Models.HomeVideo.Item Item
    {
        get
        {
            return (BiliBiliAPI.Models.HomeVideo.Item)GetValue(ItemProperty);
        }
        set
        {
            SetValue(ItemProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemProperty =
        DependencyProperty.Register("Item", typeof(BiliBiliAPI.Models.HomeVideo.Item), typeof(HotItemStyle), new PropertyMetadata(default(BiliBiliAPI.Models.HomeVideo.Item), (s, e) => 
        {
            (s as HotItemStyle)!.ViewModel = new ItemsViewModel.HotItemViewModel() { _Data = (BiliBiliAPI.Models.HomeVideo.Item)e.NewValue };
        }));


}
