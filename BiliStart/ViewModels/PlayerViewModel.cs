using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Videos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels;
public partial class PlayerViewModel:ObservableRecipient
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

    [RelayCommand]
    public void GoBack()
    {
        (App.MainWindow.Content as MainPage)!.RootFrame.GoBack();
    }
}
