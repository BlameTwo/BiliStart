using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.Contracts.Services;
using BiliStart.Services;
using BiliStart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.ViewModels;
public class TopMoreViewModel:ObservableRecipient
{
    public IHotNavigationService navigationService { get; }
    public IHotNavigationViewService navigationViewService
    {
        get;
    }

    public TopMoreViewModel(IHotNavigationService navigationService, IHotNavigationViewService navigationViewService)
    {
        IsActive = true;
        this.navigationService = navigationService;
        this.navigationViewService = navigationViewService;
    }
}
