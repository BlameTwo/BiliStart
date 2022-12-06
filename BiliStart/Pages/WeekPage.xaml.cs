using BiliStart.ViewModels.PageViewModels;
using Microsoft.UI.Xaml.Controls;


namespace BiliStart.Pages;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class WeekPage : Page
{
    public WeekViewModel ViewModel
    {
        get;
    }

    public WeekPage()
    {
        this.ViewModel = App.GetService<WeekViewModel>(); 
        this.InitializeComponent();
    }
}
