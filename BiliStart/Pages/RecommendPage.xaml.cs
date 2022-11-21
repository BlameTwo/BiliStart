using BiliBiliAPI.Video;
using BiliStart.Event;
using BiliStart.Interfaces;
using BiliStart.ViewModel;
using CommunityToolkit.Mvvm.Messaging;
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
using ListView = ZUDesignControl.Controls.ListView;

namespace BiliStart.Pages
{
    /// <summary>
    /// RecommendPage.xaml 的交互逻辑
    /// </summary>
    public partial class RecommendPage : Page, IPageBase
    {
        public RecommendPage()
        {
            InitializeComponent();
            this.DataContext = new RecommendVM();
            Loaded += RecommendPage_Loaded;
        }

        private void RecommendPage_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void RecommendPage_Loaded(object sender, RoutedEventArgs e)
        {
            var value = this.ActualWidth;
        }

        public void SetExtraData(object ExtraData)
        {

        }
    }
}
