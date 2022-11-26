// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.ViewModels.PageViewModels;
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

namespace BiliStart.Pages;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class RankPage : Page
{
    public RankViewModel ViewModel
    {
        get;
    }

    public RankPage()
    {
        ViewModel = App.GetService<RankViewModel>();
        this.InitializeComponent();
    }

    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        try
        {
            //分区导航
            // Tip:如果你第一次运行排行榜页面到了这里就报错，这里是没有错误的哦，靠Tag携带的Tid参数来区别分区还是全区，继续F5下去吧
            var value = int.Parse(e.Parameter.ToString()!);
            if(value != null)
            {
                await ViewModel!.refersh(value.ToString()!);
            }
        }
        catch (Exception)
        {
            await ViewModel.Loaded();
        }
    }
   
}
