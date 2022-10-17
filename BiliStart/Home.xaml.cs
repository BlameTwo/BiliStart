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
        }



        
    }
}
