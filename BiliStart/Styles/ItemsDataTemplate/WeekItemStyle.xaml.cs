using BiliBiliAPI.Models.TopList;
using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;




namespace BiliStart.Styles.ItemsDataTemplate;
public sealed partial class WeekItemStyle : UserControl
{
    public WeekItemStyle()
    {
        this.InitializeComponent();
    }

    public WeekItemViewModel ViewModel
    {
        get;set;
    }


    public WeekItemData Data
    {
        get=> (WeekItemData)GetValue(DataProperty);
        set=> SetValue(DataProperty, value);
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(WeekItemData), typeof(WeekItemStyle), new PropertyMetadata(default(WeekItemStyle),
            new PropertyChangedCallback((s,e)=>OnChanged(s,e))));

    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as WeekItemStyle)!.ViewModel = new WeekItemViewModel() { _Data = (WeekItemData)e.NewValue };
    }

}
