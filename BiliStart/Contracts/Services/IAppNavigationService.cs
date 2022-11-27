using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Contracts.Services
{
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

        void GoBack(AppNavigationViewsEnum ob);

        bool NavigationTo(AppNavigationViewsEnum ob, string pageKey, object? parameter = null, bool clearNavigation = false);
    }

    public enum AppNavigationViewsEnum
    {
        HotListFrame, RootFrame,ShellFrame
    }
}
