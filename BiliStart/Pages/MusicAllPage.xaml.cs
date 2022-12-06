using BiliStart.ViewModels.PageViewModels;
using Microsoft.UI.Xaml.Controls;


namespace BiliStart.Pages;
public sealed partial class MusicAllPage : Page
{
    public MusicAllViewModel ViewModel
    {
        get;
    }
    public MusicAllPage()
    {
        this.ViewModel = App.GetService<MusicAllViewModel>();
        this.InitializeComponent();
    }

    
}
