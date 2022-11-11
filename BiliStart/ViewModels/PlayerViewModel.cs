using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Videos;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ViewModels;
public class PlayerViewModel:ObservableRecipient
{
    public PlayerViewModel()
    {

    }


    private VideosContent Content;

    public VideosContent _Content
    {
        get => Content;
        set=>SetProperty(ref Content, value);
    }

}
