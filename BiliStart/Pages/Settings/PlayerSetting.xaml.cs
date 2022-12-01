// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.Contracts.Services;
using BiliStart.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace BiliStart.Pages.Settings;
public sealed partial class PlayerSetting :UserControl
{
    public PlayerSetting()
    {
        LocalSettingsService = App.GetService<ILocalSettingsService>();
        this.InitializeComponent();
        Loaded += PlayerSetting_Loaded;
    }

    private async void PlayerSetting_Loaded(object sender, RoutedEventArgs e)
    {
        var value = await LocalSettingsService.ReadSettingAsync<int>(BiliStart.Models.Settings.Player_Supper_Supper);
        if (value != null)
        {
            select.SelectedIndex = value;
        }
    }

    public ILocalSettingsService LocalSettingsService
    {
        get;
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LocalSettingsService.SaveSettingAsync(BiliStart.Models.Settings.Player_Supper_Supper, (sender as ComboBox)!.SelectedIndex);
    }
}
