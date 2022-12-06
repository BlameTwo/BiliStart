using BiliStart.ViewModels.SearchModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Styles.Search;
public sealed partial class SearchAnimation : UserControl
{
    public SearchAnimationViewModel ViewModel
    {
        get;
    }

    public SearchAnimation()
    {
        this.ViewModel = App.GetService<SearchAnimationViewModel>();
        this.InitializeComponent();
    }




    public string SearchKey
    {
        get => (string)GetValue(SearchKeyProperty);
        set => SetValue(SearchKeyProperty, value);
    }

    
    public static readonly DependencyProperty SearchKeyProperty =
        DependencyProperty.Register("SearchKey", typeof(string), typeof(SearchAnimation), new PropertyMetadata("",(s,e)=>Onchanged(s,e)));

    private static void Onchanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as SearchAnimation)!.ViewModel._SearchKey = (string)e.NewValue;
    }
}
