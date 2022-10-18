using BiliStart.ViewModel;
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

namespace BiliStart.Pages
{
    /// <summary>
    /// RecommendPage.xaml 的交互逻辑
    /// </summary>
    public partial class RecommendPage : Page
    {
        public RecommendPage()
        {
            InitializeComponent();
            this.DataContext = new RecommendVM();
            this.SizeChanged += RecommendPage_SizeChanged;
            Loaded += RecommendPage_Loaded;
        }


        private void RecommendPage_Loaded(object sender, RoutedEventArgs e)
        {
            var value = this.ActualWidth;
        }

        private void RecommendPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(this.ActualWidth < 100)
            {
                string a = "小于100";
            }
        }
    }
}
