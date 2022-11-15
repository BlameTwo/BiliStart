using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Contracts.Services;

public interface ITipShow
{
    void SendMessage(string message, string title);

    TeachingTip TipControl
    {
        get;set;    
    }
}