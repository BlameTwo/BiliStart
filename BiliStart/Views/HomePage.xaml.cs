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
using Microsoft.Windows.Widgets.Providers;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace BiliStart.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class HomePage : Page
{

    public HomeViewModel ViewModel
    {
        get;
    }
    public HomePage()
    {
        //在页面初始化完成前获得VM
        ViewModel = App.GetService<HomeViewModel>();
        this.InitializeComponent();

    }
}
