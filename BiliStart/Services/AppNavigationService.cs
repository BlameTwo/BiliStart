using BiliStart.Contracts.Services;
using BiliStart.Contracts.ViewModels;
using BiliStart.Helpers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.Services
{
    public class AppNavigationService : IAppNavigationService
    {
        private Frame shellframe, hotlistframe, rootframe,dynamicframe;
        private object? shellobj;
        private object? hotlistobj;
        private object? rootobj;
        private object? dynamicobj;

        public event NavigatedEventHandler? ShellNavigated;
        public event NavigatedEventHandler? HotListNavigated;
        public event NavigatedEventHandler? RootNavigated;
        public event NavigatedEventHandler DynamicNavigated;

        public AppNavigationService(IPageService pageService)
        {
            PageService = pageService;
        }

        private void RegisterFrameEvents(Frame obj , AppNavigationViewsEnum ob)
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
                case AppNavigationViewsEnum.DynamicFrame:
                    obj.Navigated += DynamicNavigated;
                    break;
            }
        }

        private void UnRegisterFrameEvents(Frame obj, AppNavigationViewsEnum ob)
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
                case AppNavigationViewsEnum.DynamicFrame:
                    obj.Navigated -= DynamicNavigated;
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
                UnRegisterFrameEvents(value, AppNavigationViewsEnum.HotListFrame);
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
                UnRegisterFrameEvents(value, AppNavigationViewsEnum.RootFrame);
                rootframe = value;
                RegisterFrameEvents(rootframe, AppNavigationViewsEnum.RootFrame);
            }
        }
        public Frame? ShellFrame
        {
            get
            {
                if (shellframe == null)
                {
                    RegisterFrameEvents(shellframe, AppNavigationViewsEnum.ShellFrame);
                }
                return shellframe;
            }
            set
            {
                UnRegisterFrameEvents(value, AppNavigationViewsEnum.ShellFrame);
                shellframe = value;
                RegisterFrameEvents(shellframe, AppNavigationViewsEnum.ShellFrame);
            }
        }


        public Frame? DynamicFrame
        {
            get
            {
                if (dynamicframe == null)
                {
                    RegisterFrameEvents(dynamicframe, AppNavigationViewsEnum.DynamicFrame);
                }
                return shellframe;
            }
            set
            {
                UnRegisterFrameEvents(value, AppNavigationViewsEnum.DynamicFrame);
                dynamicframe = value;

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

        public bool? CanDynamicFrameBack
        {
            get => dynamicframe.CanGoBack;
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
                case AppNavigationViewsEnum.DynamicFrame:
                    nowframe = dynamicframe;
                    nowparameter = dynamicobj;
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
                        case AppNavigationViewsEnum.DynamicFrame:
                            dynamicobj = parameter;
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

        public bool GoBack(AppNavigationViewsEnum ob)
        {
            switch (ob)
            {
                case AppNavigationViewsEnum.HotListFrame:
                    return CanBack(HotListFrame);
                case AppNavigationViewsEnum.RootFrame:
                    return CanBack(RootFrame);
                case AppNavigationViewsEnum.ShellFrame:
                    return CanBack(ShellFrame);
                case AppNavigationViewsEnum.DynamicFrame:
                    return CanBack(DynamicFrame);
            }
            return false;
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
