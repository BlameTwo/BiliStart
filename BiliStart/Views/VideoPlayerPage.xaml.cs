// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace BiliStart.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class VideoPlayerPage : Page
{
    public VideoPlayerViewModel ViewModel
    {
        get;
    }

    public VideoPlayerPage()
    {
        this.ViewModel = App.GetService<VideoPlayerViewModel>();
        this.InitializeComponent();
    }

    private void green_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        Show.Begin();
    }

    private void green_PointerExited(object sender, PointerRoutedEventArgs e)
    {
        Close.Begin();
    }
}
