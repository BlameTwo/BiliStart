using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Contracts.Services;


/// <summary>
/// 弹窗服务
/// </summary>
public interface ITipShow
{
    void SendMessage(string message, string title);

    TeachingTip TipControl
    {
        get;set;    
    }
}