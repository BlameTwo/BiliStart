using BiliBiliAPI.Models.Videos;
using BiliStart.ViewModel.ItemViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BiliStart.Controls.PlayerPivotItems
{
    /// <summary>
    /// PlayerDetailsItem.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerDetailsItem : UserControl
    {
        public PlayerDetailsItem()
        {
            InitializeComponent();
        }

        public VideosContent VideoContent
        {
            get { return (VideosContent)GetValue(VideoContentProperty); }
            set { SetValue(VideoContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VideoContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VideoContentProperty =
            DependencyProperty.Register("VideoContent", typeof(VideosContent), typeof(PlayerDetailsItem), new PropertyMetadata(default(VideosContent),new PropertyChangedCallback((s,e)=>OnChanged(s,e))));

        private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            (s as PlayerDetailsItem)!.DataContext = new PlayerDetailsItemVM() { VC = (VideosContent)e.NewValue };
        }
    }
}
