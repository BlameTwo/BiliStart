// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliBiliAPI.Models.Search;
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
public sealed partial class SearchVideoItem : UserControl
{
    public SearchVideoItem()
    {
        this.InitializeComponent();
    }

    public SearchVideoItemViewModel ViewModel
    {
        get;set;    
    }



    public BiliBiliAPI.Models.Search.Item Data
    {
        get=> (BiliBiliAPI.Models.Search.Item)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(BiliBiliAPI.Models.Search.Item), typeof(SearchVideoItem), new PropertyMetadata(default(BiliBiliAPI.Models.Search.Item),  
            (s,e)=>OnChanged(s,e)));


    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as SearchVideoItem)!.ViewModel = new SearchVideoItemViewModel() { _Data = (BiliBiliAPI.Models.Search.Item)e.NewValue};
    }
}
