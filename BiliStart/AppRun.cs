using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



        [STAThread]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Main(string[] args)
        {
            BiliStart.App app = new BiliStart.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
