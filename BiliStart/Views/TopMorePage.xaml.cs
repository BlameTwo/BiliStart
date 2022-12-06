using BiliStart.Contracts.Services;
using BiliStart.ViewModels;
using Microsoft.UI.Xaml.Controls;




namespace BiliStart.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TopMorePage : Page
{
    public TopMoreViewModel ViewModel
    {
        get;
    }
    public TopMorePage()
    {
        ViewModel = App.GetService<TopMoreViewModel>();
        this.InitializeComponent();
        ViewModel.NavigationService.HotListFrame = FrameControl;
        ViewModel.NavigationViewService.Initialize(NavigationControl, AppNavigationViewsEnum.HotListFrame);
        ViewModel.NavigationService.NavigationTo(AppNavigationViewsEnum.HotListFrame,typeof(HotViewModel).FullName!);
    }
}
