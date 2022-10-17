
using BiliStart.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BiliStart
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            InitialServices();
        }

        private static void InitialServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<QRLoginVM>();
            services.AddTransient<HomeVM>();
            services.AddTransient<PasswordLoginVM>();
            var provider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(provider);
        }
    }
}
