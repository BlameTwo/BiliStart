// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using BiliStart.ViewModels;
using Microsoft.UI.Xaml.Controls;
namespace BiliStart.Views;


public sealed partial class PGCPlayerPage : Page
{
    public PGCPlayerViewModel ViewModel
    {
        get;
    }
    public PGCPlayerPage()
    {
        this.ViewModel = App.GetService<PGCPlayerViewModel>();
        this.InitializeComponent();
    }
}
