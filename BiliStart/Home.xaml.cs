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
    public partial class Home :WindowBase
    {

        public Home()
        {
            InitializeComponent();
            this.DataContext = Ioc.Default.GetService<HomeVM>();
            RootFrame.NavigationService.LoadCompleted += NavigationService_LoadCompleted;
            RootFrame.NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            //导航后触发的列表选项变化
            Type type = e.Content.GetType();
            if(type == typeof(RecommendPage))
            {
                Recommend.IsSelected = true;
            }
            else if(type == typeof(HotPage))
            {
                Hot.IsSelected = true;
            }else if(type == typeof(TopVideoPage))
            {
                Top.IsSelected = true;
            }
            else
            {
                Recommend.IsSelected = false;
                Hot.IsSelected = false;
                Top.IsSelected = false;
            }
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //使用接口进行传递参数
            var page = e.Content as IPageBase;
            page!.SetExtraData(e.ExtraData);
        }


        private void MicaWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //搜索栏的显示控制
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
