using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Comment;
using BiliBiliAPI.Models.Comment;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media.Imaging;

namespace BiliStart.ItemsViewModel;

[INotifyPropertyChanged]
public partial class CommentItemViewModel: Comments
{
    public CommentItemViewModel()
    {
    }

    [ObservableProperty]
    bool _Islike;

    [ObservableProperty]
    int _LikeCount;

    [RelayCommand]
    void Loaded()
    {
        //赋值是否点赞
        Islike = Convert.ToBoolean(Action);
        LikeCount = Convert.ToInt32(this.Likes);
    }

    [RelayCommand]
    async void LikeCommand(bool islike)
    {
        VideoComment comment = new VideoComment();
        var result = await comment.SetCommentState(islike, this.ID, this.Oid);
        if (result.Code != "0")
        {
            //ToDo:日志写入，这里写成弹出请求报错
            App.GetService<ITipShow>().SendMessage(result.Message,Symbol.Help);
            Islike = Convert.ToBoolean(Action);
            
        }
        else
        {
            App.GetService<ITipShow>().SendMessage("操作成功!", Symbol.Add);
            Islike = true;
            if (islike)
                LikeCount++;
            else
                LikeCount--;
        }
    }
    
    
}
