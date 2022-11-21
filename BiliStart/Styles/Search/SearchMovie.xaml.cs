// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.ViewModels.SearchModels;
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

namespace BiliStart.Styles.Search;
public sealed partial class SearchMovie : UserControl
{
    public SearchMovieViewModel ViewModel
    {
        get;
    }
    public SearchMovie()
    {
        this.ViewModel = App.GetService<SearchMovieViewModel>();
        this.InitializeComponent();
    }



    public string SearchKey
    {
        get=> (string)GetValue(SearchKeyProperty);
        set => SetValue(SearchKeyProperty, value);
    }

    // Using a DependencyProperty as the backing store for SearchKey.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SearchKeyProperty =
        DependencyProperty.Register("SearchKey", typeof(string), typeof(SearchMovie), new PropertyMetadata("",(s,e)=>OnChanged(s,e)));

    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as SearchMovie)!.ViewModel._SearchKey= (string)e.NewValue;
    }
}
