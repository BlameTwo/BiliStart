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

namespace BiliStart.Login
{
    /// <summary>
    /// PasswordLogin.xaml 的交互逻辑
    /// </summary>
    public partial class PasswordLogin : UserControl
    {
        public PasswordLogin()
        {
            InitializeComponent();
            this.DataContext = Ioc.Default.GetService<PasswordLoginVM>();
        }

    }
}
