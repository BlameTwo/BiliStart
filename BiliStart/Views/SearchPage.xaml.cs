using BiliStart.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;




namespace BiliStart.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SearchPage : Page
{
    public SearchViewModel ViewModel
    {
        get;
    }
    public SearchPage()
    {
        this.ViewModel = App.GetService<SearchViewModel>();
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        //获得传参
        if(e.Parameter != null && e.Parameter is string value)
        {
            ViewModel._SearchKey = value;
        }
        base.OnNavigatedTo(e);
    }
}
