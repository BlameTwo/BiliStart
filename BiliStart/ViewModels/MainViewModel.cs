using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.Contracts.Services;
using BiliStart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.ViewModels;
public partial class MainViewModel : ObservableRecipient
{
    public MainViewModel(INavigationService navigationService, IHotNavigationService hotNavigationService)
    {
        NavigationService = navigationService;
        HotNavigationService = hotNavigationService;
        NavigationService.Navigated += OnNavigated;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
    
    }
    public INavigationService NavigationService
    {
        get;
    }

    public IHotNavigationService HotNavigationService
    {
        get;
    }

}
