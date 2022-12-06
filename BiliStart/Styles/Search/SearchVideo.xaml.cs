using BiliStart.ViewModels.SearchModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace BiliStart.Styles.Search;
public sealed partial class SearchVideo : UserControl
{
    public SearchVideoViewModel ViewModel
    {
        get;
    }

    public SearchVideo()
    {
        this.ViewModel = App.GetService<SearchVideoViewModel>();
        this.InitializeComponent();
        Loaded += SearchVideo_Loaded;
    }





    public string SearchKey
    {
        get => (string)GetValue(SearchKeyProperty);
        set => SetValue(SearchKeyProperty, value);
    }

    // Using a DependencyProperty as the backing store for SearchKey.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SearchKeyProperty =
        DependencyProperty.Register("SearchKey", typeof(string), typeof(SearchVideo), new PropertyMetadata("",(s,e)=>Onchanged(s,e)));

    private static void Onchanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as SearchVideo).ViewModel._SearchKey = (string)e.NewValue;
    }

    private void SearchVideo_Loaded(object sender, RoutedEventArgs e)
    {
    
    }
}
