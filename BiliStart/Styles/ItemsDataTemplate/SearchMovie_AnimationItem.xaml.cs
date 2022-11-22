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

namespace BiliStart.Styles.ItemsDataTemplate;


public sealed partial class SearchMovie_AnimationItem : UserControl
{
    public SearchMovieItemViewModel ViewModel
    {
        get;set;
    }


    public SearchMovie_AnimationItem()
    {
        this.InitializeComponent();

    }

    public Aniation_Movie_Item Data
    {
        get=> (Aniation_Movie_Item)GetValue(DataProperty);
        set=> SetValue(DataProperty, value);
    }

    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(Aniation_Movie_Item), typeof(SearchMovie_AnimationItem), new PropertyMetadata(default(Aniation_Movie_Item),(s,e)=>Onchanged(s,e)));

    private static void Onchanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as SearchMovie_AnimationItem)!.ViewModel = new SearchMovieItemViewModel() { _Data = (Aniation_Movie_Item)e.NewValue };
    }
}
