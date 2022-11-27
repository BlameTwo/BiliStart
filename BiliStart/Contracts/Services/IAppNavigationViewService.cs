using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Contracts.Services;

public interface IAppNavigationViewService
{
    void Initialize(NavigationView navigationView, AppNavigationViewsEnum ob);


    void UnregisterEvents(AppNavigationViewsEnum appNavigationViewsEnum);

    NavigationViewItem? GetSelectedItem(Type pageType);

    object? SettingsItem
    {
        get;
    }

    IList<object>? MenuItems
    {
        get;
    }
}
