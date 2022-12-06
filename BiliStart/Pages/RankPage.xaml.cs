using BiliStart.ViewModels.PageViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;


namespace BiliStart.Pages;
public sealed partial class RankPage : Page
{
    public RankViewModel ViewModel
    {
        get;
    }

    public RankPage()
    {
        ViewModel = App.GetService<RankViewModel>();
        this.InitializeComponent();
    }

    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        try
        {
            //分区导航
            // Tip:如果你第一次运行排行榜页面到了这里就报错，这里是没有错误的哦，靠Tag携带的Tid参数来区别分区还是全区，继续F5下去吧
            var value = int.Parse(e.Parameter.ToString()!);
            if(value != null)
            {
                await ViewModel!.refersh(value.ToString()!);
            }
        }
        catch (Exception)
        {
            await ViewModel.Loaded();
        }
    }

}
