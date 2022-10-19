using BilibiliAPI.Video;
using BiliBiliAPI.Models.HomeVideo;
using BiliBiliAPI.Models.Videos;
using BiliStart.Controls;
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
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using FontFamily = System.Windows.Media.FontFamily;
using Page = System.Windows.Controls.Page;

namespace BiliStart.Pages
{
    /// <summary>
    /// Player.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerPage : Page
    {
        private HomeVideoVM item;
        private DispatcherTimer timer= new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1)};
        public PlayerPage(HomeVideoVM Item)
        {
            InitializeComponent();
            Unosquare.FFME.Library.FFmpegDirectory = "D:\\FFmpeg";
            Loaded += PlayerPage_Loaded;
            item = Item;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                durition.Text = media.Position.ToString(@"hh\:mm\:ss\.fff");
                slider.Value = media.Position.TotalSeconds;
            });
            var nowpositon = media.Position.TotalSeconds;
            var templist = formatDanmakuTextModels.Where(p => p.Time > nowpositon && p.Time - nowpositon < 1).ToList();
            foreach (var item in templist)
            {
                SolidColorBrush color = new SolidColorBrush();
                var text = "#" + System.Convert.ToString(System.Convert.ToInt32(item.Color.ToString(), 16) + 40000, 16);
                var style = new DankumuTextStyle()
                {
                    Color = new SolidColorBrush(Colors.White),
                    Size = item.FontSize,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("微软雅黑")
                };
                switch (item.DanmakuType)
                {
                    case "5":
                        dankumu.CreateTopText(item.Text,style);
                        break;
                    case "1":
                        dankumu.CreateScrollText(item.Text, style);
                        break;
                    case "2":
                        dankumu.CreateScrollText(item.Text, style);
                        break;
                    case "3":
                        dankumu.CreateScrollText(item.Text, style);
                        break;
                    case "4":
                        dankumu.CreateBootomText(item.Text, style);
                        break;
                }
            }
        }


        List<FormatDanmakuTextModel> formatDanmakuTextModels = new List<FormatDanmakuTextModel>();


        private async void PlayerPage_Loaded(object sender, RoutedEventArgs e)
        {
            BilibiliAPI.Video.Video video = new BilibiliAPI.Video.Video();
            var value =  await video.GetVideosContent(item._Item.PlayArg.Aid, BiliBiliAPI.Models.VideoIDType.AV);
            //赛博朋克边缘行者4K视频
            //var value =  await video.GetVideosContent("BV1LB4y1E7iD", BiliBiliAPI.Models.VideoIDType.BV);
            var download = await video.GetVideo(value.Data, BiliBiliAPI.Models.VideoIDType.AV, BiliBiliAPI.Models.DashEnum.Dash1080P60);
            await media.Open(new Uri(download.Data.Durl[0].Url));
            Danmaku danmaku = new Danmaku();
            var danmakutext = await danmaku.GetDanmakuTest(value.Data.First_Cid);
            formatDanmakuTextModels = await danmaku.GetFormatDanmakuText(danmakutext);
        }

        private void MediaElement_MediaInitializing(object sender, MediaInitializingEventArgs e)
        {
            e.Configuration.PrivateOptions.Add("referer", "https://bilibili.com");
            e.Configuration.PrivateOptions["user_agent"] = $"{typeof(ContainerConfiguration).Namespace}/{typeof(ContainerConfiguration).Assembly.GetName().Version}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
        }

        private void media_MediaOpened(object sender, MediaOpenedEventArgs e)
        {
            durition.Text = e.Info.Duration.ToString(@"hh\:mm\:ss\.fff");
            slider.Maximum = e.Info.Duration.TotalSeconds;
        }
    }
}
