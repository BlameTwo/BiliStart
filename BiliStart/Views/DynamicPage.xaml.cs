// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.ViewModels;
using Microsoft.UI.Input;
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

namespace BiliStart.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DynamicPage : Page
{
    public DynamicViewModel ViewModel
    {
        get;
    }
    public DynamicPage()
    {
        this.ViewModel = App.GetService<DynamicViewModel>();
        this.InitializeComponent();
        this.ViewModel.NavigationService.DynamicFrame = FrameControl;
        this.ViewModel.NavigationViewService.Initialize(navigation, Contracts.Services.AppNavigationViewsEnum.DynamicFrame);
    }

   
}
