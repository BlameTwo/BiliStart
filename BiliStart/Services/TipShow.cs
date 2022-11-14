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
    public async void SendMessage(string message, string title)
    {
        var result = (App.MainWindow.Content as MainPage)!.ToggleThemeTeachingTip2;
        result.Title = title;
        result.Subtitle = message;
        result.IsOpen = true;
        await Task.Delay(TimeSpan.FromSeconds(2));
        result.IsOpen = false;
    }
}
