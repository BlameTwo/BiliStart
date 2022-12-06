using Microsoft.UI.Xaml.Controls;
using BiliStart.ViewModels;

namespace BiliStart.Views
{
    public sealed partial class HotPage : Page
    {
        public HotPage()
        {
            ViewModel = App.GetService<HotViewModel>();
            this.InitializeComponent();
        }


        public HotViewModel ViewModel
        {
            get;
        }
    }
}
