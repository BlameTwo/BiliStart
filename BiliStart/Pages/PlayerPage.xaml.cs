using BilibiliAPI.Video;
using BiliBiliAPI.Models.HomeVideo;
using BiliBiliAPI.Models.Videos;
using BiliStart.Controls;
using BiliStart.Interfaces;
using BiliStart.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Threading;
using Unosquare.FFME.Common;
using ZUDesignControl.Controls;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using FontFamily = System.Windows.Media.FontFamily;
using MessageBox = ZUDesignControl.Controls.MessageBox;
using Page = System.Windows.Controls.Page;

namespace BiliStart.Pages
{
    /// <summary>
    /// Player.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerPage : Page,IPageBase
    {
        private VideosContent item;
        UserVideo userVideo = new UserVideo();

        public PlayerPage()
        {
            InitializeComponent();
            Loaded += PlayerPage_Loaded;
            SizeChanged += PlayerPage_SizeChanged;
        }

        private void PlayerPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(this.ActualWidth < 900)
            {
                more.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(player, 2);
            }
            else
            {
                more.Visibility = Visibility.Visible;

                Grid.SetColumnSpan(player, 1);
            }
        }

        private async void PlayerPage_Loaded(object sender, RoutedEventArgs e)
        {
            player.VideoContent = item;
        }

        private async void islike_Checked(object sender, RoutedEventArgs e)
        {
            bool value = (bool)(sender as EmojiSwitchButton)!.IsChecked!;
            var result =  await userVideo.LikeVideo(value, item.Aid);
            MessageBox.Show("提示",result.Data.TipText);
        }

        public void SetExtraData(object ExtraData)
        {
            item = (VideosContent)ExtraData;

            islike.IsChecked = item.ReqUser.Like == "1" ? true : false;
        }
    }
}
