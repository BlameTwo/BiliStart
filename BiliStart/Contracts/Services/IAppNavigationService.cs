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
        /// <summary>
        /// 主页面导航
        /// </summary>
        Frame? ShellFrame
        {
            get;set;
        }

        /// <summary>
        /// 排行榜导航
        /// </summary>
        Frame? HotListFrame
        {
            get;set;
        }

        /// <summary>
        /// 应用根节点导航
        /// </summary>
        Frame? RootFrame
        {
            get;set;
        }

        /// <summary>
        /// 动态导航
        /// </summary>
        Frame? DynamicFrame
        {
            get;set;
        }

        /// <summary>
        /// 主页面是否支持回退
        /// </summary>
        bool? CanShellFrameBack 
        {
            get;
        }

        /// <summary>
        /// 排行榜页面是否支持回退
        /// </summary>
        bool? CanHotListFrameBack
        {
            get;
        }

        /// <summary>
        /// 根节点页面是否支持回退
        /// </summary>
        bool? CanRootFrameBack
        {
            get;
        }

        /// <summary>
        /// 动态页面是否支持回退
        /// </summary>
        bool? CanDynamicFrameBack
        {
            get;
        }

        /// <summary>
        /// 主页面导航结束后触发
        /// </summary>
        event NavigatedEventHandler ShellNavigated;
        /// <summary>
        /// 根节点导航结束后触发
        /// </summary>
        event NavigatedEventHandler RootNavigated;
        /// <summary>
        /// 排行榜导航结束后触发
        /// </summary>
        event NavigatedEventHandler HotListNavigated;

        /// <summary>
        /// 动态导航结束后触发
        /// </summary>
        event NavigatedEventHandler DynamicNavigated;

        /// <summary>
        /// 综合回退
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        bool GoBack(AppNavigationViewsEnum ob);

        /// <summary>
        /// 综合前进
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        bool GoForward(AppNavigationViewsEnum ob);

        /// <summary>
        /// 综合导航
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="pageKey"></param>
        /// <param name="parameter"></param>
        /// <param name="clearNavigation"></param>
        /// <returns></returns>
        bool NavigationTo(AppNavigationViewsEnum ob, string pageKey, object? parameter = null, bool clearNavigation = false);


    }

    /// <summary>
    /// 导航枚举
    /// </summary>
    public enum AppNavigationViewsEnum
    {
        HotListFrame, RootFrame,ShellFrame,DynamicFrame
    }
}
