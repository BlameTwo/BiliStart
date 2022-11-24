﻿using BiliBiliAPI.Account;
using BiliStart.Contracts.Services;
using BiliStart.Dialogs;
using BiliStart.Helpers;
using BiliStart.ViewModels;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Graphics;
using Windows.System;
using WinRT.Interop;

namespace BiliStart.Views;

// TODO: Update NavigationViewItem titles and icons in ShellPage.xaml.
public sealed partial class ShellPage : Page
{

    public ShellViewModel ViewModel
    {
        get;set;
    }
    public ShellPage()
    {
        ViewModel = App.GetService<ShellViewModel>();
        InitializeComponent();
        ViewModel.NavigationService.Frame = NavigationFrame;
        
        ViewModel.NavigationViewService.Initialize(NavigationViewControl);
        App.MainWindow.Activated += MainWindow_Activated;
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        AppTitleBarText.Text = "AppDisplayName".GetLocalized();
        ViewModel.FlyoutButton = userbutton;
    }



    private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        TitleBarHelper.UpdateTitleBar(RequestedTheme);
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
        KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
        ViewModel.FlyoutButton = userbutton;

        App.MainWindow.SetTitleBar(AppTitleBar);
    }

    private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
    {
        var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";
        
        AppTitleBarText.Foreground = (SolidColorBrush)App.Current.Resources[resource];
    }

    private void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
    {
        AppTitleBar.Margin = new Thickness()
        {
            Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2 : 1),
            Top = AppTitleBar.Margin.Top,
            Right = AppTitleBar.Margin.Right,
            Bottom = AppTitleBar.Margin.Bottom
        };
        if(sender.DisplayMode == NavigationViewDisplayMode.Minimal)
        {
            grid.Margin = new Thickness(10, 45, 10, 10);
        }
        else
        {
            grid.Margin = new Thickness(10);
        }
    }

   

    private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
    {
        var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

        if (modifiers.HasValue)
        {
            keyboardAccelerator.Modifiers = modifiers.Value;
        }

        keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

        return keyboardAccelerator;
    }


    private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
    {
        var navigationService = App.GetService<INavigationService>();

        var result = navigationService.GoBack();

        args.Handled = result;
    }

    private void NavigationViewControl_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if(this.ActualWidth < 650)
        {
            NavigationViewControl.PaneDisplayMode = NavigationViewPaneDisplayMode.LeftMinimal;
            NavigationViewControl.IsPaneToggleButtonVisible = true;
        }
        else
        {
            NavigationViewControl.PaneDisplayMode = NavigationViewPaneDisplayMode.Left;

            NavigationViewControl.IsPaneToggleButtonVisible = false;
        }
    }

    private async void NavigationFrame_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {
        await ViewModel.InitSearch();
    }
}
