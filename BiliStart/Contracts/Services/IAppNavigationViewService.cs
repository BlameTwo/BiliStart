using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Contracts.Services;

/// <summary>
/// App导航绑定
/// </summary>
public interface IAppNavigationViewService
{
    void Initialize(NavigationView navigationView, AppNavigationViewsEnum ob);


    void UnregisterEvents(AppNavigationViewsEnum appNavigationViewsEnum);

    NavigationViewItem? GetSelectedItem(AppNavigationViewsEnum appNavigationViewsEnum, Type pageType);

    object? SettingsItem
    {
        get;
    }

    IList<object>? MenuItems
    {
        get;
    }
}
