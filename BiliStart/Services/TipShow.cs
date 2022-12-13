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
    private Panel _fatherPanel;
    public Panel FatherPanel
    {
        get => _fatherPanel;
        set => _fatherPanel = value;
    }

    public async void SendMessage(string message, Symbol icon)
    {
        BiliStart.UI.Controls.PopupDialog popup = new(message, FatherPanel, icon);
        popup.ShowAPopup();
    }
}
