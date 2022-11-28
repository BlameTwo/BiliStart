using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BiliStart.Contracts.Services;
using BiliStart.Helpers;
using BiliStart.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Popups;

namespace BiliStart.Services
{
    public class AppNavigationViewService : IAppNavigationViewService
    {
        NavigationView shellview, rootview, hotview;

        public AppNavigationViewService(IAppNavigationService appNavigationService,IPageService pageService)
        {
            AppNavigationService = appNavigationService;
            PageService = pageService;
        }

        public object? SettingsItem => shellview.SettingsItem;


        public IList<object>? MenuItems => shellview.MenuItems;


        public IAppNavigationService AppNavigationService
        {
            get;
        }
        public IPageService PageService
        {
            get;
        }

        public NavigationViewItem? GetSelectedItem(AppNavigationViewsEnum appNavigationViewsEnum, Type pageType)
        {
            switch (appNavigationViewsEnum)
            {
                case AppNavigationViewsEnum.HotListFrame:
                    return GetSelectedItem(hotview.MenuItems, pageType) ?? GetSelectedItem(hotview.MenuItems, pageType);
                case AppNavigationViewsEnum.RootFrame:
                    return GetSelectedItem(rootview.MenuItems, pageType) ?? GetSelectedItem(rootview.FooterMenuItems, pageType);
                case AppNavigationViewsEnum.ShellFrame:
                    return GetSelectedItem(shellview.MenuItems, pageType) ?? GetSelectedItem(shellview.FooterMenuItems, pageType);
                default:
                    return null;
            }
        }

        public void Initialize(NavigationView navigationView, AppNavigationViewsEnum ob)
        {
            switch (ob)
            {
                case AppNavigationViewsEnum.HotListFrame:
                    hotview= navigationView;
                    hotview.ItemInvoked += Hotview_ItemInvoked;
                    hotview.BackRequested += Hotview_BackRequested;
                    break;
                case AppNavigationViewsEnum.RootFrame:
                    rootview= navigationView;
                    rootview.ItemInvoked += Rootview_ItemInvoked;
                    rootview.BackRequested += Rootview_BackRequested;
                    break;
                case AppNavigationViewsEnum.ShellFrame:
                    shellview= navigationView;
                    shellview.ItemInvoked += Shellview_ItemInvoked;
                    shellview.BackRequested += Shellview_BackRequested;
                    break;
            }
            
        }


        private NavigationViewItem? GetSelectedItem(IEnumerable<object> menuItems, Type pageType)
        {
            foreach (var item in menuItems.OfType<NavigationViewItem>())
            {
                if (IsMenuItemForPageType(item, pageType))
                {
                    return item;
                }

                var selectedChild = GetSelectedItem(item.MenuItems, pageType);
                if (selectedChild != null)
                {
                    return selectedChild;
                }
            }

            return null;
        }

        private bool IsMenuItemForPageType(NavigationViewItem menuItem, Type sourcePageType)
        {
            if (menuItem.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
            {
                return PageService.GetPageType(pageKey) == sourcePageType;
            }

            return false;
        }

        private void Hotview_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            AppNavigationService.GoBack(AppNavigationViewsEnum.HotListFrame);
        }
        private void Hotview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ItemInvoked(AppNavigationViewsEnum.HotListFrame,args);
        }

        private void Shellview_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            AppNavigationService.GoBack(AppNavigationViewsEnum.ShellFrame);
        }

        private void Shellview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ItemInvoked(AppNavigationViewsEnum.ShellFrame, args);
        }

        private void Rootview_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            AppNavigationService.GoBack( AppNavigationViewsEnum.RootFrame);
        }

        private void Rootview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ItemInvoked(AppNavigationViewsEnum.RootFrame,args);
        }

        void ItemInvoked(AppNavigationViewsEnum viewenum, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                if(viewenum== AppNavigationViewsEnum.ShellFrame)
                    AppNavigationService.NavigationTo(viewenum, typeof(SettingsViewModel).FullName!);
            }
            else
            {
                var selectedItem = args.InvokedItemContainer as NavigationViewItem;

                if (selectedItem?.GetValue(NavigationHelper.NavigateToProperty) is string pageKey)
                {
                    AppNavigationService.NavigationTo(viewenum, pageKey);
                }
            }
        }

        public void UnregisterEvents(AppNavigationViewsEnum appNavigationViewsEnum)
        {
            switch (appNavigationViewsEnum)
            {
                case AppNavigationViewsEnum.HotListFrame:
                    hotview.ItemInvoked -= Hotview_ItemInvoked;
                    hotview.BackRequested-= Hotview_BackRequested;
                    break;
                case AppNavigationViewsEnum.RootFrame:
                    rootview.ItemInvoked -= Rootview_ItemInvoked;
                    rootview.BackRequested -= Rootview_BackRequested;
                    break;
                case AppNavigationViewsEnum.ShellFrame:
                    shellview.ItemInvoked -= Shellview_ItemInvoked;
                    shellview.BackRequested -= Shellview_BackRequested;
                    break;
            }
        }
        
    }
}
