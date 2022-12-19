using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.Contracts.Services;
using BiliStart.ViewModels;
using BiliStart.ViewModels.Models;
using BiliStart.Views;
using Microsoft.UI.Dispatching;

namespace BiliStart.Services;
internal class GoVideo : IGoVideo
{
    PlayerArgs playargs;
    public PlayerArgs PlayerArgs
    {
        get => playargs;
        set=> playargs = value;
    }
    public IAppNavigationService AppNavigationService
    {
        get;
    }

    public GoVideo(IAppNavigationService appNavigationService)
    {
        AppNavigationService = appNavigationService;
    }

    public bool Go()
    {
        if(PlayerArgs != null)
        {
            App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
            {
                AppNavigationService.NavigationTo(AppNavigationViewsEnum.RootFrame, typeof(PlayerViewModel).FullName!, playargs);
            }); 
        }
        return false;
    }

    public bool GoPGC()
    {
        //PGC跳转
        return true;
    }
}
