using BiliStart.ViewModels.SearchModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;


namespace BiliStart.Styles.Search;
public sealed partial class SearchMovie : UserControl
{
    public SearchMovieViewModel ViewModel
    {
        get;
    }
    public SearchMovie()
    {
        this.ViewModel = App.GetService<SearchMovieViewModel>();
        this.InitializeComponent();
    }



    public string SearchKey
    {
        get=> (string)GetValue(SearchKeyProperty);
        set => SetValue(SearchKeyProperty, value);
    }

    // Using a DependencyProperty as the backing store for SearchKey.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SearchKeyProperty =
        DependencyProperty.Register("SearchKey", typeof(string), typeof(SearchMovie), new PropertyMetadata("",(s,e)=>OnChanged(s,e)));

    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as SearchMovie)!.ViewModel._SearchKey= (string)e.NewValue;
    }
}
