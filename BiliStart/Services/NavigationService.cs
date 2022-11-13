using System.Diagnostics.CodeAnalysis;

using BiliStart.Contracts.Services;
using BiliStart.Contracts.ViewModels;
using BiliStart.Helpers;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.Services;

public class NavigationService : INavigationService
{
    private readonly IPageService _pageService;
    private object? _lastParameterUsed;
    private object? root_lastParameterUsed;
    private Frame? _frame;
    private Frame? _rootframe;
    public event NavigatedEventHandler? Navigated;
    public event NavigatedEventHandler? RootNavigated;
    public Frame? Frame
    {
        get
        {
            if (_frame == null)
            {
                _frame = App.MainWindow.Content as Frame;
                RegisterFrameEvents();
            }

            return _frame;
        }

        set
        {
            UnregisterFrameEvents();
            _frame = value;
            RegisterFrameEvents();
        }
    }

    [MemberNotNullWhen(true, nameof(Frame), nameof(_frame))]
    public bool CanGoBack => Frame != null && Frame.CanGoBack;


    [MemberNotNullWhen(true, nameof(RootFrame), nameof(_rootframe))]
    public bool RootCanGoBack => RootFrame != null && RootFrame.CanGoBack;


    public Frame? RootFrame
    {
        get
        {
            if (_rootframe == null)
            {
                _rootframe = App.MainWindow.Content as Frame;
                RootRegisterFrameEvents();
            }

            return _rootframe;
        }

        set
        {
            RootUnregisterFrameEvents();
            _rootframe = value;
            RootRegisterFrameEvents();
        }
    }

    private void RootUnregisterFrameEvents()
    {

        if (_rootframe != null)
        {
            _rootframe.Navigated -= RootOnNavigated;
        }
    }

    private void RootOnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame)
        {
            var clearNavigation = false;
            if (clearNavigation)
            {
                _rootframe.BackStack.Clear();
            }

            if (_rootframe.GetPageViewModel() is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.Parameter);
            }

            Navigated?.Invoke(sender, e);
        }
    }

    private void RootRegisterFrameEvents()
    {
        if (_rootframe != null)
        {
            _rootframe.Navigated += RootOnNavigated;
        }
    }

    public NavigationService(IPageService pageService)
    {
        _pageService = pageService;
    }

    private void RegisterFrameEvents()
    {
        if (_frame != null)
        {
            _frame.Navigated += OnNavigated;
        }
    }

    private void UnregisterFrameEvents()
    {
        if (_frame != null)
        {
            _frame.Navigated -= OnNavigated;
        }
    }

    public bool GoBack()
    {
        if (CanGoBack)
        {
            var vmBeforeNavigation = _frame.GetPageViewModel();
            _frame.GoBack();
            if (vmBeforeNavigation is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedFrom();
            }

            return true;
        }

        return false;
    }


    public bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false)
    {
        var pageType = _pageService.GetPageType(pageKey);

        if (_frame != null && (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed))))
        {
            _frame.Tag = clearNavigation;
            var vmBeforeNavigation = _frame.GetPageViewModel();
            var navigated = _frame.Navigate(pageType, parameter);
            if (navigated)
            {
                _lastParameterUsed = parameter;
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }
            }

            return navigated;
        }

        return false;
    }

    public bool RootNavigationTo(string pageKey, object? parameter = null, bool clearNavigation = false)
    {
        var pageType = _pageService.GetPageType(pageKey);

        if (_rootframe != null && (_rootframe.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(root_lastParameterUsed))))
        {
            _rootframe.Tag = clearNavigation;
            var vmBeforeNavigation = _rootframe.GetPageViewModel();
            var navigated = _rootframe.Navigate(pageType, parameter);
            if (navigated)
            {
                root_lastParameterUsed = parameter;
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }
            }

            return navigated;
        }

        return false;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame)
        {
            var clearNavigation = (bool)frame.Tag;
            if (clearNavigation)
            {
                frame.BackStack.Clear();
            }

            if (frame.GetPageViewModel() is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.Parameter);
            }

            Navigated?.Invoke(sender, e);
        }
    }

    public bool RootBack()
    {
        if (RootCanGoBack)
        {
            var vmBeforeNavigation = _rootframe.GetPageViewModel();
            _frame.GoBack();
            if (vmBeforeNavigation is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedFrom();
            }
            return true;
        }
        return false;
    }
}
