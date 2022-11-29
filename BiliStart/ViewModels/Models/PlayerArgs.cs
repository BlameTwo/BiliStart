using BiliBiliAPI.Models.Videos;

namespace BiliStart.ViewModels.Models;
public class PlayerArgs
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
    Video,Animation,Movie,
}
