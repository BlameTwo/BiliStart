using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.Contracts.Services;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Services
{
    public class AppNavigationViewService : IAppNavigationViewService
    {
        Dictionary<string,Frame> FrameValues=new Dictionary<string,Frame>();

        public AppNavigationViewService(IAppNavigationService appNavigationService,IPageService pageService)
        {
            AppNavigationService = appNavigationService;
            PageService = pageService;


            InitFrame();
        }

        private void InitFrame()
        {
            
        }

        public object? SettingsItem => throw new NotImplementedException();

        public IList<object>? MenuItems => throw new NotImplementedException();

        public IAppNavigationService AppNavigationService
        {
            get;
        }
        public IPageService PageService
        {
            get;
        }

        public NavigationViewItem? GetSelectedItem(Type pageType) => throw new NotImplementedException();
        public void Initialize(NavigationView navigationView, AppNavigationViewsEnum ob) => throw new NotImplementedException();
        public void UnregisterEvents() => throw new NotImplementedException();
    }
}
