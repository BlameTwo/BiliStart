using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;




namespace BiliStart.Styles.ItemsDataTemplate;
public sealed partial class SearchVideoItem : UserControl
{
    public SearchVideoItem()
    {
        this.InitializeComponent();
    }

    public SearchVideoItemViewModel ViewModel
    {
        get;set;    
    }



    public BiliBiliAPI.Models.Search.Item Data
    {
        get=> (BiliBiliAPI.Models.Search.Item)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(BiliBiliAPI.Models.Search.Item), typeof(SearchVideoItem), new PropertyMetadata(default(BiliBiliAPI.Models.Search.Item),  
            (s,e)=>OnChanged(s,e)));


    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as SearchVideoItem)!.ViewModel = new SearchVideoItemViewModel() { _Data = (BiliBiliAPI.Models.Search.Item)e.NewValue};
    }
}
