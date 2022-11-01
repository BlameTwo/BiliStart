using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZUDesignControl.Controls;
using BiliStart.Dialog;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Models.Settings;
using BilibiliAPI.Account;
using BilibiliAPI;
using System.IO;
using CommunityToolkit.Mvvm.DependencyInjection;
using BiliStart.ViewModel;
using BiliStart.Pages;
using BiliStart.Interfaces;

namespace BiliStart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Home :MicaWindow
    {

        public Home()
        {
            InitializeComponent();
            this.DataContext = Ioc.Default.GetService<HomeVM>();
            RootFrame.NavigationService.LoadCompleted += NavigationService_LoadCompleted;
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            var page = e.Content as IPageBase;
            page!.SetExtraData(e.ExtraData);
        }


        private void MicaWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualWidth < 750)
            {
                titbarcontrol.Visibility = Visibility.Collapsed;
            }
            else
            {
                titbarcontrol.Visibility = Visibility.Visible;
            }
        }

        private void navigation_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue is NavigationItem item)
            {
                switch (item.Tag)
                {
                    case "推荐":
                        RootFrame.NavigationService.Navigate(new RecommendPage());
                        break;
                    case "热门":
                        RootFrame.NavigationService.Navigate(new HotPage());
                        break;
                    case "排行榜":
                        RootFrame.NavigationService.Navigate(new TopVideoPage());
                        break;
                }
            }
        }
    }
}
