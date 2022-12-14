using BiliBiliAPI.Models.Search;
using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Styles.ItemsDataTemplate;


public sealed partial class SearchMovie_AnimationItem : UserControl
{
    public SearchMovieItemViewModel ViewModel
    {
        get;set;
    }


    public SearchMovie_AnimationItem()
    {
        this.InitializeComponent();

    }

    public Aniation_Movie_Item Data
    {
        get=> (Aniation_Movie_Item)GetValue(DataProperty);
        set=> SetValue(DataProperty, value);
    }

    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(Aniation_Movie_Item), typeof(SearchMovie_AnimationItem), new PropertyMetadata(default(Aniation_Movie_Item),(s,e)=>Onchanged(s,e)));

    private static void Onchanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as SearchMovie_AnimationItem)!.ViewModel = new SearchMovieItemViewModel() { _Data = (Aniation_Movie_Item)e.NewValue };
    }
}
