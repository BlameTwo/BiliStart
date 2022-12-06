using BiliStart.ViewModels.PageViewModels;
using Microsoft.UI.Xaml.Controls;


namespace BiliStart.Pages;
public sealed partial class MustWatchPage : Page
{
    public MustWatchViewModel ViewModel
    {
        get;
    }

    public MustWatchPage()
    {
        ViewModel = App.GetService<MustWatchViewModel>();   
        this.InitializeComponent();
    }
}
