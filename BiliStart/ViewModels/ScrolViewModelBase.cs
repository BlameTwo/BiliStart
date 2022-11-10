using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace BiliStart.ViewModels;
public class ScrolViewModelBase:ObservableRecipient
{
    public ScrolViewModelBase()
    {
        ScrollLoad = new RelayCommand<AdaptiveGridView>((arg) => Scroolload(arg));
    }

    private void Scroolload(AdaptiveGridView? arg)
    {
        var listview = (VisualTreeHelper.GetChild(arg, 0) as Border)!.Child as ScrollViewer;
        SV = listview;
        SV.ViewChanged += SV_ViewChanged;
    }

    private async void SV_ViewChanged(object? sender, ScrollViewerViewChangedEventArgs e)
    {
        SV.ViewChanged -= SV_ViewChanged;
        var sv = sender as ScrollViewer;
        var flage = sv.VerticalOffset + sv.ViewportHeight;

        if (sv.ExtentHeight - flage < 5 && sv.ViewportHeight != 0)
        {

            await AddData.ExecuteAsync(null);
        }

        SV.ViewChanged += SV_ViewChanged;
    }

    /// <summary>
    /// 滚动条滚动到极限后
    /// </summary>
    public AsyncRelayCommand AddData
    {
        get;set;    
    }

    public RelayCommand<AdaptiveGridView> ScrollLoad
    {
        get; set;
    }
    public ScrollViewer? SV
    {
        get;
         set;
    }
}
