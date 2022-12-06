using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BiliStart.ItemsViewModel;
using BiliBiliAPI.Models.TopList;

namespace BiliStart.Styles.ItemsDataTemplate;
public sealed partial class MustWatchItemStyle : UserControl
{
    public MustWatchItemViewModel ViewModel
    {
        get; set;
    }

    public MustWatchItemStyle()
    {
        this.InitializeComponent();
    }




    public MustWatchDataItem Item
    {
        get
        {
            return (MustWatchDataItem)GetValue(ItemProperty);
        }
        set
        {
            SetValue(ItemProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ItemProperty =
        DependencyProperty.Register("Item", typeof(MustWatchDataItem), typeof(MustWatchItemStyle), new PropertyMetadata(null, (s, e) => OnChanged(s, e)));

    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        (s as MustWatchItemStyle).ViewModel = new MustWatchItemViewModel() { _Data = (MustWatchDataItem)e.NewValue };
    }
}
