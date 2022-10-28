using BiliStart.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BiliStart
{
    public class AppRun: System.Windows.Application
    {

        private bool _contentLoaded;


        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;

            this.StartupUri = new System.Uri("Home.xaml", System.UriKind.Relative);

            System.Uri resourceLocater = new System.Uri("/BiliStart;component/app.xaml", System.UriKind.Relative);

            System.Windows.Application.LoadComponent(this, resourceLocater);
        }

        private static void App_Startup(object sender, StartupEventArgs e)
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
            services.AddTransient<TopVideoVM>();
            services.AddTransient<SearchPageVM>();
            var provider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(provider);
        }

        static BiliStart.App  app = new BiliStart.App();

        [STAThread]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Main(string[] args)
        {
            app.Startup += App_Startup;
            app.InitializeComponent();
            app.Run();
        }
    }
}
