using BiliBiliAPI;
using BiliBiliAPI.Video;
using BiliBiliAPI.Models.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using Unosquare.FFME;
using Unosquare.FFME.Common;
using ZUDesignControl.Controls;
using UserControl = System.Windows.Controls.UserControl;

namespace BiliStart.Controls
{
    /// <summary>
    /// PlayerMediaCotrol.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerMediaControl : UserControl,IDisposable
    {
        VideosContent VC { get; set; }
        VideoInfo VideoInfo { get; set; }
        bool IsPlay { get; set; }
        DispatcherTimer Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(30) };

        public PlayerMediaControl()
        {
            InitializeComponent();
            media.MediaInitializing += Media_MediaInitializing;
            mediavideo.MediaInitializing += Media_MediaInitializing;
            Loaded += PlayerMediaCotrol_Loaded;
            timer.Tick += Timer_Tick;
            media.MediaOpened += Media_MediaOpened;
            //Unloaded += PlayerMediaControl_Unloaded;
            Timer.Tick += Timer_Tick1;
            timer.Start();
        }

        UserVideo userVideo = new UserVideo();
        private double OldTick { get; set; } = 0;
        private void Timer_Tick1(object? sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(async() =>
            {

                var value = media.Position.Ticks;
                if (value > 0 && OldTick != value)
                {
                    OldTick = value;

                    var result = await userVideo.PostProgress(int.Parse(VC.Aid), VC.First_Cid, media.Position);
                }
            }));
        }

        private async void Media_MediaOpened(object? sender, MediaOpenedEventArgs e)
        {
            slider.Maximum = media.NaturalDuration!.Value.TotalSeconds;
            timer.Start();
        }

        private async void PlayerMediaControl_Unloaded(object sender, RoutedEventArgs e)
        {
            await media.Close();
            await mediavideo.Close(); 
            Loaded -= PlayerMediaCotrol_Loaded;
            timer.Tick -= Timer_Tick;
            //Unloaded += PlayerMediaControl_Unloaded;
            timer = null;
        }

        BiliBiliAPI.Video.Video video = new BiliBiliAPI.Video.Video();


        public VideosContent VideoContent
        {
            get { return (VideosContent)GetValue(VideoContentProperty); }
            set { SetValue(VideoContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VideoContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VideoContentProperty =
            DependencyProperty.Register("VideoContent", typeof(VideosContent), typeof(PlayerMediaControl), new PropertyMetadata(default(VideosContent),new PropertyChangedCallback((s,e)=>ContentChanged(s,e))));

        private static void ContentChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            (s as PlayerMediaControl)!.VC = (e.NewValue as VideosContent) ?? null;
        }

        private async void PlayerMediaCotrol_Loaded(object sender, RoutedEventArgs e)
        {
            Danmaku danmaku = new Danmaku();
            var danmakutext = await danmaku.GetDanmakuTest(VC.First_Cid);
            formatDanmakuTextModels = await danmaku.GetFormatDanmakuText(danmakutext);
            VideoInfo =  (await video.GetVideo(VC, BiliBiliAPI.Models.VideoIDType.BV, 0)).Data;
            support.ItemsSource = VideoInfo.Support_Formats;
            support.SelectedIndex = 0;
        }


        List<FormatDanmakuTextModel> formatDanmakuTextModels = new List<FormatDanmakuTextModel>();

        private void Media_MediaInitializing(object? sender, MediaInitializingEventArgs e)
        {
            e.Configuration.PrivateOptions.Add("referer", "https://bilibili.com");
            e.Configuration.PrivateOptions["user_agent"] = $"{typeof(ContainerConfiguration).Namespace}/{typeof(ContainerConfiguration).Assembly.GetName().Version}";
        }

        private DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        private bool disposedValue;

        private void Timer_Tick(object? sender, EventArgs e)
        {

            Dispatcher.Invoke(async () =>
            {
                durition.Text = media.Position.ToString(@"hh\:mm\:ss")+"\\"+ media.NaturalDuration!.Value.ToString(@"hh\:mm\:ss");
                slider.Value = media.Position.TotalSeconds;
            });
            var nowpositon = media.Position.TotalSeconds;
            var danmakulist = formatDanmakuTextModels.Where(p => p.Time > nowpositon && p.Time - nowpositon < 1).ToList();
            foreach (var item in danmakulist)
            {
                SolidColorBrush color = new SolidColorBrush();
                var text = "#" + System.Convert.ToString(System.Convert.ToInt32(item.Color.ToString()), 16);
                var style = new DankumuTextStyle()
                {
                    Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(text)),
                    Size = item.FontSize,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("微软雅黑")
                };
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

        public void Play()
        {
            media.Play();
            mediavideo.Play();
            timer.Start();
        }

        public void Pause()
        {
            
        }

        public void Stop()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private async void support_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var value = (sender as SplitButton).SelectedItem as Support_Formats;
            TimeSpan videotime = media.Position.Ticks == 0 ? new TimeSpan() :media.Position;
            media.Pause();
            mediavideo.Pause();
            foreach (var item in VideoInfo.Dash.DashVideos)
            {
                if(item.ID == value.Quality)
                {
                    media.Open(new Uri(item.Base_Url));
                    mediavideo.Open(new Uri(VideoInfo.Dash.DashAudio[0].BaseUrl));
                    if(videotime.Ticks!=0)
                    {
                        media.Seek(videotime);
                        mediavideo.Seek(videotime);
                        media.Play();
                        mediavideo.Play();
                    }
                    break;
                }
            }
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    timer.Stop();
                    Timer.Stop();
                    timer.Tick -= Timer_Tick1;
                    Timer.Tick -= Timer_Tick;
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~PlayerMediaControl()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

}
