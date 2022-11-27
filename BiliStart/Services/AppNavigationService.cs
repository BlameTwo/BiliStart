using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.Contracts.Services;
using BiliStart.Contracts.ViewModels;
using BiliStart.Helpers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.Services
{
    public class AppNavigationService : IAppNavigationService
    {
        private Frame shellframe, hotlistframe, rootframe;
        private object? shellobj;
        private object? hotlistobj;
        private object? rootobj;
        public event NavigatedEventHandler? ShellNavigated;
        public event NavigatedEventHandler? HotListNavigated;
        public event NavigatedEventHandler? RootNavigated;

        public Frame? ShellFrame
        {
            get
            {
                if(shellframe == null)
                {
                    RegisterFrameEvents(shellframe, AppNavigationViewsEnum.ShellFrame);
                }
                return shellframe;
            }
            set
            {
                UnRegisterFrameEvents(shellframe, AppNavigationViewsEnum.ShellFrame);
                shellframe = value;
                RegisterFrameEvents(shellframe, AppNavigationViewsEnum.ShellFrame);
            }
        }

        public AppNavigationService(IPageService pageService)
        {
            PageService = pageService;
        }

        private void RegisterFrameEvents(Frame obj , AppNavigationViewsEnum ob)
        {
            switch (ob)
            {
                case AppNavigationViewsEnum.HotListFrame:
                    obj.Navigated -= HotListNavigated;
                    break;
                case AppNavigationViewsEnum.RootFrame:
                    obj.Navigated -= RootNavigated;
                    break;
                case AppNavigationViewsEnum.ShellFrame:
                    obj.Navigated -= ShellNavigated;
                    break;
            }
        }

        private void UnRegisterFrameEvents(Frame obj, AppNavigationViewsEnum ob)
        {
            switch (ob)
            {
                case AppNavigationViewsEnum.HotListFrame:
                    obj.Navigated += HotListNavigated;
                    break;
                case AppNavigationViewsEnum.RootFrame:
                    obj.Navigated += RootNavigated;
                    break;
                case AppNavigationViewsEnum.ShellFrame:
                    obj.Navigated += ShellNavigated;
                    break;
            }
        }

        public Frame? HotListFrame
        {
            get
            {
                if (shellframe == null)
                {
                    RegisterFrameEvents(hotlistframe, AppNavigationViewsEnum.HotListFrame);
                }
                return hotlistframe;
            }
            set
            {
                UnRegisterFrameEvents(hotlistframe, AppNavigationViewsEnum.HotListFrame);
                hotlistframe = value;
                RegisterFrameEvents(hotlistframe, AppNavigationViewsEnum.HotListFrame);
            }
        }
        public Frame? RootFrame
        {
            get
            {
                if (shellframe == null)
                {
                    RegisterFrameEvents(rootframe, AppNavigationViewsEnum.RootFrame);
                }
                return rootframe;
            }
            set
            {
                UnRegisterFrameEvents(rootframe, AppNavigationViewsEnum.RootFrame);
                rootframe = value;
                RegisterFrameEvents(rootframe, AppNavigationViewsEnum.RootFrame);
            }
        }


        public bool? CanShellFrameBack
        {
            get => shellframe.CanGoBack;
        }

        public bool? CanHotListFrameBack
        {
            get => hotlistframe.CanGoBack;
        }

        public bool? CanRootFrameBack
        {
            get => rootframe.CanGoBack;
        }
        public IPageService PageService
        {
            get;
        }
        

        public bool NavigationTo(AppNavigationViewsEnum ob, string pageKey, object? parameter = null, bool clearNavigation = false)
        {
            Frame nowframe = null;
            object nowparameter = null;
            switch (ob)
            {
                case AppNavigationViewsEnum.HotListFrame:
                    nowframe = hotlistframe;
                    nowparameter = hotlistobj;
                    break;
                case AppNavigationViewsEnum.RootFrame:
                    nowframe = rootframe;
                    nowparameter = rootobj;
                    break;
                case AppNavigationViewsEnum.ShellFrame:
                    nowframe = shellframe;
                    nowparameter = shellobj;
                    break;
                default:
                    break;
            }
            var pageType = PageService.GetPageType(pageKey);

            if (nowframe != null && (nowframe.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(nowparameter))))
            {
                nowframe.Tag = clearNavigation;
                var vmBeforeNavigation = nowframe.GetPageViewModel();
                var navigated = nowframe.Navigate(pageType, parameter);
                if (navigated)
                {
                    switch (ob)
                    {
                        case AppNavigationViewsEnum.HotListFrame:
                            hotlistobj = parameter;
                            break;
                        case AppNavigationViewsEnum.RootFrame:
                            rootobj = parameter;
                            break;
                        case AppNavigationViewsEnum.ShellFrame:
                            hotlistobj = parameter;
                            break;
                    }
                    if (vmBeforeNavigation is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatedFrom();
                    }
                }

                return navigated;
            }

            return false;
        }

        public void GoBack(AppNavigationViewsEnum ob)
        {
            switch (ob)
            {
                case AppNavigationViewsEnum.HotListFrame:
                    CanBack(HotListFrame);
                    break;
                case AppNavigationViewsEnum.RootFrame:
                    CanBack(RootFrame);
                    break;
                case AppNavigationViewsEnum.ShellFrame:
                    CanBack(ShellFrame);
                    break;
            }
        }

        private bool CanBack(Frame? frame)
        {
            if (frame.CanGoBack)
            {
                var vmBeforeNavigation = frame.GetPageViewModel();
                frame.GoBack();
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }
                return true;
            }

            return false;
        }
    }
}
