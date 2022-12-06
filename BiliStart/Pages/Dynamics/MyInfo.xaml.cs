


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BiliStart.ViewModels.PageViewModels.DynamicsViewModels;

namespace BiliStart.Pages.Dynamics
{
    public sealed partial class MyInfoPage : Page
    {
        public MyInfoViewModel ViewModel
        {
            get;
        }

        public MyInfoPage()
        {
            this.ViewModel = App.GetService<MyInfoViewModel>();
            this.InitializeComponent();
        }

    }
}
