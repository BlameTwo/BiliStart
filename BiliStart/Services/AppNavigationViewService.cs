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

        public NavigationViewItem? GetSelectedItem(Type pageType) => throw new NotImplementedException();

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

        private void Hotview_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            AppNavigationService.GoBack(AppNavigationViewsEnum.HotListFrame);
        }
        private void Hotview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            ItemInvoked(AppNavigationViewsEnum.HotListFrame,args);
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
                    AppNavigationService.NavigationTo(viewenum, typeof(SettingsViewModel).FullName!);
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
