using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;




namespace BiliStart.Styles.ItemsDataTemplate;
public sealed partial class HomeItemStyle : UserControl
{
    public HomeItemStyle()
    {
        this.InitializeComponent();
    }

    public HomeItemViewModel ViewModel{get; private set; }


    public BiliBiliAPI.Models.HomeVideo.HomeDataItem Item
    {
        get=>(BiliBiliAPI.Models.HomeVideo.HomeDataItem)GetValue(ItemProperty);
        
        set =>SetValue(ItemProperty, value);
    }

    // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemProperty =
        DependencyProperty.Register("Item", typeof(BiliBiliAPI.Models.HomeVideo.HomeDataItem), typeof(HomeItemStyle), new PropertyMetadata(default(BiliBiliAPI.Models.HomeVideo.HomeDataItem),new PropertyChangedCallback((s,e)=>OnChanged(s,e))));

    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        if(e.NewValue != null && e.NewValue is BiliBiliAPI.Models.HomeVideo.HomeDataItem item)
        {
            (s as HomeItemStyle)!.ViewModel = new HomeItemViewModel() { _Item = item };
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
    }
}
