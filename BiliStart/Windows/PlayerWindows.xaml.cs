using BilibiliAPI.Video;
using BiliBiliAPI.Models.Videos;
using BiliStart.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Unosquare.FFME.Common;
using ZUDesignControl.Controls;
using MessageBox = System.Windows.MessageBox;

namespace BiliStart.Windows
{
    /// <summary>
    /// PlayerWindows.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerWindows : AcrylicBlurWindow
    {
        VideosContent VC { get; set; }

        private List<FormatDanmakuTextModel> formatDanmakuTextModels;

        VideoInfo VideoInfo { get; set; }
        bool IsPlay { get; set; }
        DispatcherTimer PostProcess = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(30) };
        DispatcherTimer DanmakuPostProcess = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        UserVideo userVideo = new UserVideo();

        public bool Playing { get; set; }

        public bool OkPlay { get; set; }
    
        private double OldTick { get; set; } = 0;

        BilibiliAPI.Video.Video video = new BilibiliAPI.Video.Video();

        public PlayerWindows(VideosContent item)
        {
            InitializeComponent();
            media.MediaInitializing += Media_MediaInitializing;
            mediavideo.MediaInitializing += Media_MediaInitializing;
            Loaded += PlayerWindows_Loaded;
            media.MediaOpened += Media_MediaOpened;
            media.MediaOpened+=Media_MediaOpened;
            PostProcess.Tick += PostProcess_Tick1;
            DanmakuPostProcess.Tick += Danmaku_Tick;
            VC = item;
            SizeChanged += PlayerWindows_SizeChanged;
            Playing = false;
        }


        private void PlayerWindows_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.ActualWidth < 900)
            {
                more.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(fathergrid, 2);
            }
            else
            {
                more.Visibility = Visibility.Visible;
                Grid.SetColumnSpan(fathergrid, 1);
            }
        }

        private async void PlayerWindows_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Danmaku danmaku = new Danmaku();
                var danmakutext = await danmaku.GetDanmakuTest(VC.First_Cid);
                formatDanmakuTextModels = await danmaku.GetFormatDanmakuText(danmakutext);
                VideoInfo = (await video.GetVideo(VC, BiliBiliAPI.Models.VideoIDType.BV, 0)).Data;
                support.ItemsSource = VideoInfo.Support_Formats;
                support.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PostProcess_Tick1(object? sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(async () =>
            {

                var value = media.Position.Ticks;
                if (value > 0 && OldTick != value)
                {
                    OldTick = value;

                    var result = await userVideo.PostProgress(int.Parse(VC.Aid), VC.First_Cid, media.Position);
                }
            }));
        }

        private void Danmaku_Tick(object? sender, EventArgs e)
        {
            Dispatcher.Invoke(async () =>
            {
                durition.Text = media.Position.ToString(@"hh\:mm\:ss") + "\\" + media.NaturalDuration!.Value.ToString(@"hh\:mm\:ss");
                slider.Value = media.Position.TotalSeconds;
            });
            var nowpositon = media.Position.TotalSeconds;
            var danmakulist = formatDanmakuTextModels.Where(p => p.Time > nowpositon && p.Time - nowpositon < 1).ToList();
            foreach (var item in danmakulist)
            {
                SolidColorBrush color = new SolidColorBrush();
                var text = "";
                try
                {
                    text = "#" + System.Convert.ToString(System.Convert.ToInt32(item.Color.ToString()), 16);
                }
                catch (Exception)
                {
                    text = "令牌无效";
                }
                var style = new DankumuTextStyle()
                {
                    Size = item.FontSize,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("微软雅黑")
                };
                style.Color = text != "令牌无效" ? new SolidColorBrush((Color)ColorConverter.ConvertFromString(text)) : new SolidColorBrush(Colors.White);
                //增加弹幕
                switch (item.DanmakuType)
                {
                    case "5":
                        dankumu.CreateTopText(item.Text, style);
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

        private async void support_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = (sender as SplitButton)!.SelectedItem as Support_Formats;
            TimeSpan videotime = media.Position.Ticks == 0 ? new TimeSpan() : media.Position;
            await media.Pause();
            await mediavideo.Pause();
            foreach (var item in VideoInfo.Dash.DashVideos)
            {
                if (item.ID == value!.Quality)
                {
                    var value2 =  await media.Open(new Uri(item.Base_Url));
                    var value3 =  await mediavideo.Open(new Uri(VideoInfo.Dash.DashAudio[0].BaseUrl));
                    if(value3 && value2)
                    {
                        Play();
                    }
                    if (videotime.Ticks != 0)
                    {
                        await media.Seek(videotime);
                        await mediavideo.Seek(videotime);
                    }
                    break;
                }
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// 此事件共挂载两个触发者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_MediaOpened(object? sender, MediaOpenedEventArgs e)
        {
            if (media.IsOpen == true && mediavideo.IsOpen == true)
            {
                playbutton.IsEnabled = true;
                slider.Maximum = media.NaturalDuration!.Value.TotalSeconds;
                DanmakuPostProcess.Start();
                PostProcess.Start();
            }
            else
            {
                playbutton.IsEnabled = false;
            }
        }

        private void Media_MediaInitializing(object? sender, MediaInitializingEventArgs e)
        {
            e.Configuration.PrivateOptions.Add("referer", "https://bilibili.com");
            e.Configuration.PrivateOptions["user_agent"] = $"{typeof(ContainerConfiguration).Namespace}/{typeof(ContainerConfiguration).Assembly.GetName().Version}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }

        public async void Play()
        {
            if(media.Source != null)
                slider.Maximum = media.NaturalDuration!.Value.TotalSeconds;
            Playing = !Playing;
            if (Playing)
            {
                await media.Play();
                await mediavideo.Play();
                DanmakuPostProcess.Start();
                playbutton.Content = "\uE769";
                PostProcess.Start();
            }
            else
            {
                await media.Pause();
                await mediavideo.Pause();
                DanmakuPostProcess.Stop();
                PostProcess.Stop();
                playbutton.Content = "\uE768";
            }
        }
    }
}
