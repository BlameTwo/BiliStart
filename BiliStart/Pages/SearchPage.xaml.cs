using BiliStart.Interfaces;
using BiliStart.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;
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
    /// SearchPage.xaml 的交互逻辑
    /// </summary>
    public partial class SearchPage : Page,IPageBase
    {
        public SearchPage()
        {
            InitializeComponent();
            
        }

        public void SetExtraData(object ExtraData)
        {
            this.DataContext = new SearchPageVM() { _SearchKey = (string)ExtraData };
        }

    }
}
