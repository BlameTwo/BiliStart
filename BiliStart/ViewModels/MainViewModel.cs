using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.ViewModels;
public partial class MainViewModel : ObservableRecipient
{
    public MainViewModel(IAppNavigationService appNavigationService,ITipShow tipShow)
    {
        AppNavigationService = appNavigationService;
        TipShow = tipShow;

    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
    
    }
    public IAppNavigationService AppNavigationService
    {
        get;
    }
    public ITipShow TipShow
    {
        get;set;
    }
}
