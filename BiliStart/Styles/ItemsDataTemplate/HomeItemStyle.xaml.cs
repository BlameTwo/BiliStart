// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.ItemsViewModel;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.WebUI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BiliStart.Styles.ItemsDataTemplate;
public sealed partial class HomeItemStyle : UserControl
{
    public HomeItemStyle()
    {
        this.InitializeComponent();
    }

    public HomeItemViewModel ViewModel{get; private set; }


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
        DependencyProperty.Register("Item", typeof(BiliBiliAPI.Models.HomeVideo.Item), typeof(HomeItemStyle), new PropertyMetadata(default(BiliBiliAPI.Models.HomeVideo.Item),new PropertyChangedCallback((s,e)=>OnChanged(s,e))));

    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        if(e.NewValue != null && e.NewValue is BiliBiliAPI.Models.HomeVideo.Item item)
        {
            (s as HomeItemStyle)!.ViewModel = new HomeItemViewModel() { _Item = item };
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        image.Opacity = 1;
    }
}
