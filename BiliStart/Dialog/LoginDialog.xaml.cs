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
using ZUDesignControl.Controls;

namespace BiliStart.Dialog
{
    /// <summary>
    /// LoginDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDialog : DialogHost
    {
        public LoginDialog()
        {
            InitializeComponent();
            this.DataContext = Ioc.Default.GetService<LoginViewModel>();
            (DataContext as LoginViewModel)!.dialoghost = this;
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
