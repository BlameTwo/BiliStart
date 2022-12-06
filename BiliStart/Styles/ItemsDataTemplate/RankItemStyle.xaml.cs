using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Styles.ItemsDataTemplate;
public sealed partial class RankItemStyle : UserControl
{
    public RankItemStyle()
    {
        this.InitializeComponent();
    }

    public RankItemViewModel ViewModel
    {
        get;set;
    }


    public BiliBiliAPI.Models.TopList.TopVideo Data
    {
        get => (BiliBiliAPI.Models.TopList.TopVideo)GetValue(DataProperty);
        set =>SetValue(DataProperty, value);
    }

    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(BiliBiliAPI.Models.TopList.TopVideo), typeof(RankItemStyle), new PropertyMetadata(default(BiliBiliAPI.Models.TopList.TopVideo),
            new PropertyChangedCallback((s, e) =>
            {
                (s as RankItemStyle)!.ViewModel = new RankItemViewModel() { _Video = (BiliBiliAPI.Models.TopList.TopVideo)e.NewValue };
            })));


}
