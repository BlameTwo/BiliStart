// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliBiliAPI.Models.TopList;
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
public sealed partial class MusicRankItemStyle : UserControl
{
    public MusicRankItemStyle()
    {
        this.InitializeComponent();
    }

    public MusicRankItemViewModel ViewModel
    {
        get;set;
    }



    public MusicRankData Data
    {
        get
        {
            return (MusicRankData)GetValue(DataProperty);
        }
        set
        {
            SetValue(DataProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(MusicRankData), typeof(MusicRankItemStyle), new PropertyMetadata(null, 
            (s,e)=>Onchanged(s,e)));

    private static void Onchanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as MusicRankItemStyle)!.ViewModel = new() { _Data = (MusicRankData)e.NewValue };
    }

}
