using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Videos;

namespace BiliStart.ViewModels.Models;
public struct PlayerArgs
{
    public VideosContent Content
    {
        get;set;    
    }

    public GoToType Type
    {
        get;set;    
    }

    public long Aid
    {
        get;set;    

    }

    public string Bvid
    {
        get;set;
    }
}

public enum GoToType
{
    Home,User
}
