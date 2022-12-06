using BiliBiliAPI.Models.TopList;
using BiliStart.ItemsViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;




namespace BiliStart.Styles.ItemsDataTemplate;
public sealed partial class MusicRankItemStyle : UserControl
{
    public MusicRankItemStyle()
    {
        this.InitializeComponent();
    }

    public MusicRankItemViewModel ViewModel
    {
        get;set;
    }



    public MusicRankData Data
    {
        get
        {
            return (MusicRankData)GetValue(DataProperty);
        }
        set
        {
            SetValue(DataProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(MusicRankData), typeof(MusicRankItemStyle), new PropertyMetadata(null, 
            (s,e)=>Onchanged(s,e)));

    private static void Onchanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as MusicRankItemStyle)!.ViewModel = new() { _Data = (MusicRankData)e.NewValue };
    }

}
