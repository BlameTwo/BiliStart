using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.Contracts.Services;
using BiliStart.Views;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Services;
public class TipShow : ITipShow
{
    private TeachingTip teachingTip;
    public TeachingTip TipControl
    {
        get =>teachingTip;
        set => teachingTip = value;
    }


    public async void SendMessage(string message, string title)
    {
        teachingTip.Title = title;
        teachingTip.Subtitle = message;
        teachingTip.IsOpen = true;
        await Task.Delay(TimeSpan.FromSeconds(2));
        teachingTip.IsOpen = false;
    }
}
