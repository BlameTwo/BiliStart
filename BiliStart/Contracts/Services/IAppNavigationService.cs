using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.Contracts.Services
{
    /// <summary>
    /// APP导航
    /// </summary>
    public interface IAppNavigationService
    {
        Frame? ShellFrame
        {
            get;set;
        }

        Frame? HotListFrame
        {
            get;set;
        }

        Frame? RootFrame
        {
            get;set;
        }

        Frame? DynamicFrame
        {
            get;set;
        }

        bool? CanShellFrameBack 
        {
            get;
        }

        bool? CanHotListFrameBack
        {
            get;
        }

        bool? CanRootFrameBack
        {
            get;
        }

        bool? CanDynamicFrameBack
        {
            get;
        }

        event NavigatedEventHandler ShellNavigated;
        event NavigatedEventHandler RootNavigated;
        event NavigatedEventHandler HotListNavigated;
        event NavigatedEventHandler DynamicNavigated;

        bool GoBack(AppNavigationViewsEnum ob);

        bool NavigationTo(AppNavigationViewsEnum ob, string pageKey, object? parameter = null, bool clearNavigation = false);


    }

    public enum AppNavigationViewsEnum
    {
        HotListFrame, RootFrame,ShellFrame,DynamicFrame
    }
}
