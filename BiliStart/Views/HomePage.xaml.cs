using BiliStart.ViewModels;
using Microsoft.UI.Xaml.Controls;


namespace BiliStart.Views;
public sealed partial class HomePage : Page
{

    public HomeViewModel ViewModel
    {
        get;
    }
    public HomePage()
    {
        //在页面初始化完成前获得VM
        ViewModel = App.GetService<HomeViewModel>();
        this.InitializeComponent();

    }
}
