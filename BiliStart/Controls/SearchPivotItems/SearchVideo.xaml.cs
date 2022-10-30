using BiliStart.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BiliStart.Controls.SearchPivotItems
{
    /// <summary>
    /// SearchVideo.xaml 的交互逻辑
    /// </summary>
    public partial class SearchVideo : UserControl
    {
        public SearchVideo()
        {
            InitializeComponent();
            Unloaded += SearchVideo_Unloaded;
        }

        private void SearchVideo_Unloaded(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = null;
        }

        public string SearchKey
        {
            get { return (string)GetValue(SearchKeyProperty); }
            set { SetValue(SearchKeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchKeyProperty =
            DependencyProperty.Register("SearchKey", typeof(string), typeof(SearchVideo), new PropertyMetadata("",
                new PropertyChangedCallback((s, e) =>
                {
                    (s as SearchVideo).DataContext = new SearchVideoVM((string)e.NewValue);
                })));


    }
}
