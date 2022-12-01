// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.Contracts.Services;
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

namespace BiliStart.Pages.Settings;
public sealed partial class PlayerAutoStart : UserControl
{
    ILocalSettingsService LocalSettingsService
    {
        get;
    }
    public PlayerAutoStart()
    {
        this.LocalSettingsService = App.GetService<ILocalSettingsService>();    
        this.InitializeComponent();
        Loaded += PlayerAutoStart_Loaded;
    }

    private async void PlayerAutoStart_Loaded(object sender, RoutedEventArgs e)
    {
        selection.SelectedIndex =await LocalSettingsService.ReadSettingAsync<int>(BiliStart.Models.Settings.Player_AutoStart);
    }

    private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await LocalSettingsService.SaveSettingAsync(BiliStart.Models.Settings.Player_AutoStart, (sender as ComboBox)!.SelectedIndex);
    }
}
