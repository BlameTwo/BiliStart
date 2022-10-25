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

        private void NavigationItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Recommend_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.NavigationService.Navigate(new RecommendPage());
        }

        private void Hot_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.NavigationService.Navigate(new HotPage());
        }

        private void NavigationItem_Click_1(object sender, RoutedEventArgs e)
        {
            RootFrame.NavigationService.Navigate(new TopVideoPage());
        }
    }
}
