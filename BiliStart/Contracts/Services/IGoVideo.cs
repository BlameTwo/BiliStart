using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.ViewModels.Models;

namespace BiliStart.Contracts.Services;
public interface IGoVideo
{
    PlayerArgs PlayerArgs
    {
        get;set;
    }

    bool Go();
}
