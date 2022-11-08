using BiliStart.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Text;

namespace BiliStart.Views;

public sealed partial class BlankPage : Page
{
    public BlankViewModel ViewModel
    {
        get;
    }

    public BlankPage()
    {
        ViewModel = App.GetService<BlankViewModel>();
        InitializeComponent();
    }

    private void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        danmaku.CreateScrollText("这是一条弹幕", new UI.DanmakuTextStyle() 
        {
            Color = new Microsoft.UI.Xaml.Media.SolidColorBrush(Colors.Red),
            Size = 14,
            FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("微软雅黑"),
            FontWeight = FontWeights.Bold
        });;
    }
}
