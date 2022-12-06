using BiliStart.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }



    public MainPage()
    {
        this.ViewModel = App.GetService<MainViewModel>();
        this.InitializeComponent();
        ViewModel!.TipShow.TipControl = this.ToggleThemeTeachingTip2;
        ViewModel!.AppNavigationService.RootFrame = RootFrame;
    }

}
