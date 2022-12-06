using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.ViewModels.Models;

namespace BiliStart.Contracts.Services;

/// <summary>
/// 导航到视频播放
/// </summary>
public interface IGoVideo
{
    PlayerArgs PlayerArgs
    {
        get;set;
    }

    bool Go();
}
