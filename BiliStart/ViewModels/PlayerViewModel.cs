using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using BiliBiliAPI.Models;
using BiliBiliAPI.Models.User;
using BiliBiliAPI.Models.Videos;
using BiliBiliAPI.User;
using System.Linq;
using BiliBiliAPI.Video;
using BiliStart.Contracts.Services;
using BiliStart.Helpers;
using BiliStart.Services;
using BiliStart.UI;
using BiliStart.ViewModels.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Text;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.VisualStudio.Services.Commerce;
using Windows.Media.Core;
using Windows.Media.Playback;
using BiliBiliAPI.Comment;
using BiliBiliAPI.Models.Comment;
using CommunityToolkit.WinUI.UI.Controls;
using BiliStart.ItemsViewModel;

namespace BiliStart.ViewModels;
public partial class PlayerViewModel : ObservableRecipient
{
    public PlayerViewModel(ILocalSettingsService localSettingsService, ITipShow tipShow)
    {
        _FullButtonText = "\uE740";
        LocalSettingsService = localSettingsService;
        TipShow = tipShow;
        IsLike = false;
        this.NowDanmuList = new();
        ScrollLoad = new RelayCommand<ItemsControl>((arg) => Scroolload(arg));
        AddData = new AsyncRelayCommand(() => CommentAdd());
        CommentsLists = new();
    }


    public DanmakuControl DanmakuControl
    {
        get; set;
    }

    

    public DispatcherTimer? DanmuProcessTime;
    public MediaPlayer NowMediaPlayer = new();
    public MediaSource? Source;
    UserVideo? UVideo = new();
    Users User = new();
    VideoComment Comment { get; } = new();

    public PlayerArgs Args
    {
        get; set;
    }


    partial void OnVideoContentChanged(VideosContent value)
    {
        if (value.VideoDEsc.Length > 25)
        {
            VideoDesc = value.VideoDEsc.Substring(0, 25);
        }
        else
        {
            VideoDesc = value.VideoDEsc;
        }
    }

    #region 命令区域
    [RelayCommand]
    public async void LikeVideo()
    {
        IsLike = !IsLike;
        var value = (await UVideo.LikeVideo(IsLike, VideoContent.Aid)).Data;
        TipShow.SendMessage
            (
            value.TipText
            , Symbol.Message
            );
    }

    partial void OnIsPlayChanged(bool value)
    {
        if (value)
        {
            NowMediaPlayer.Play();
            return;
        }
        NowMediaPlayer.Pause();
    }

    [RelayCommand(CanExecute = nameof(CanNext))]
    public async void NextPage()
    {
        PageSelectIndex++;
    }

    [RelayCommand]
    public async void GiveCoinsVideo(int coin)
    {
        var result =  await UVideo.CoinsVideo(coin,VideoContent.Aid);
        if(result.Code == "0")
        {
            //投币成功
            IsCoins = true;
            TipShow.SendMessage($"{result.Message},{result.Data.Guide.Title}", Symbol.Message);
        }
    }

    [RelayCommand]
    private async void AddFavourite(FavoritesDataList data)
    {
        if (data != null)
        {
            var result = await UVideo.AddOrDelFavorites(VideoContent.Aid, data.ID, !Convert.ToBoolean(int.Parse(data.FavState)));
            //刷新收藏列表
            Favourites = (await User.GetFavourites(VideoContent.Aid)).Data.List.ToObservableCollection();
            foreach (var item in Favourites)
            {
                if (item.FavState == "1")
                {
                    IsFavourites = true;
                    break;
                }
                IsFavourites = false;
            }
        }
    }


    [RelayCommand]
    public void GoBack()
    {
        (App.MainWindow.Content as MainPage)!.RootFrame.GoBack();
    }
    #endregion



    #region 属性定义区域

    [ObservableProperty]
    private ObservableCollection<CommentItemViewModel> _CommentsLists;

    [ObservableProperty]
    private string _VideoDesc;

    [ObservableProperty]
    private double _SliderValue;

    [ObservableProperty]
    private VideosContent _VideoContent;

    [ObservableProperty]
    private double _MaxValue;

    [ObservableProperty]
    private bool _IsLike;

    [ObservableProperty]
    private bool _IsCoins;

    [ObservableProperty]
    private string _FullButtonText;

    [ObservableProperty]
    private ObservableCollection<Support_Formats> _Supports;

    [ObservableProperty]
    private ObservableCollection<FavoritesDataList> _Favourites;

    [ObservableProperty]
    private bool _IsFavourites;

    [ObservableProperty]
    private int _SupportIndex;

    [ObservableProperty]
    private int _PageSelectIndex;

    Support_Formats SelectFormats=null;
    public readonly BiliBiliAPI.Video.Video Video = new();

    [ObservableProperty]
    private ObservableCollection<BiliBiliAPI.Models.Videos.Page> _VideoPages;

    [ObservableProperty]
    ObservableCollection<FormatDanmakuTextModel> _NowDanmuList;

    public VideoInfo VI = null;

    [ObservableProperty]
    bool _IsPlay;

    int CommentIndex = 1;

    bool CanNext
    {
        get
        {
            if (this.VideoPages.Count == 1)
                return false;
            else
                return true;
        }
    }

    #endregion
    public void FullChanged(bool isfull)
    {
        switch (isfull)
        {
            case true:
                FullButtonText = "\uE73F";
                break;
            case false:
                FullButtonText = "\uE740";
                break;
        }
    }

    BiliBiliAPI.Video.Danmaku Danmaku = new();
    public async void InitVideo(ViewModels.Models.PlayerArgs playerArgs)
    {
        //初始化视频
        this.VideoPages = playerArgs.Content.Pages.ToObservableCollection();
        VideoContent = playerArgs.Content;
        this.Favourites = (await User.GetFavourites(VideoContent.Aid)).Data.List.ToObservableCollection() ?? new();
        IsLike = Convert.ToBoolean(VideoContent.ReqUser.Like);
        this.IsCoins = Convert.ToBoolean(VideoContent.ReqUser.Coin);
        if (VideoContent.Pages.Count == 1)
        {
            this.VI = (await Video.GetVideo(VideoContent, VideoIDType.AV, VideoContent.First_Cid)).Data;
            Supports = VI.Support_Formats.ToObservableCollection();
            //为一个播放列表直接播放。
            SupportIndex = -1;
            RefershSupport();
        }
        else
        {
            PageSelectIndex = 0;
        }
        var result = (await Comment.GetComment(this.VideoContent.Aid, CommentIndex)).Data.CommentLists.ToObservableCollection() ;
        foreach (var item in result)
        {
            
            CommentsLists.Add(GetCommentItemViewModel(item));
        }
        NowMediaPlayer.MediaOpened += NowMediaPlayer_MediaOpened;
        CommentIndex++;
    }

    CommentItemViewModel GetCommentItemViewModel(BiliBiliAPI.Models.Comment.Comments comment)
    {
       
        var lite = new CommentItemViewModel()
        {
            ReplyControl = comment.ReplyControl,
            Action = comment.Action,
            Attr = comment.Attr,
            Content = comment.Content,
            Count = comment.Count,
            CreateTime = comment.CreateTime,
            Dialog = comment.Dialog,
            Fansgrade = comment.Fansgrade,
            ID = comment.ID,
            ID_Str = comment.ID_Str,
            Likes = comment.Likes,
            Mid = comment.Mid,
            Oid = comment.Oid,
            Parent = comment.Parent,
            Rcount = comment.Rcount,
            Root = comment.Root,
            State = comment.State,
            Type = comment.Type,
            Up = comment.Up,
            CardLabel = comment.CardLabel,
            CommentLists = comment.CommentLists,
        };
        return lite;
    }

    private async Task CommentAdd()
    {
        var result = (await Comment.GetComment(this.VideoContent.Aid, CommentIndex)).Data.CommentLists.ToObservableCollection();
        foreach (var item in result)
        {
            CommentsLists.Add(GetCommentItemViewModel(item));
        }
        CommentIndex++;
    }

    private void Scroolload(ItemsControl? arg)
    {
        if (arg == null)
            return;
        var listview = VisualTreeHelper.GetChild(arg, 0) as ScrollViewer;
        SV = listview;
        SV!.ViewChanged += SV_ViewChanged;
    }

    private async void SV_ViewChanged(object? sender, ScrollViewerViewChangedEventArgs e)
    {
        SV.ViewChanged -= SV_ViewChanged;
        var sv = sender as ScrollViewer;
        var flage = sv.VerticalOffset + sv.ViewportHeight;

        if (sv.ExtentHeight - flage < 5 && sv.ViewportHeight != 0)
        {

            await AddData.ExecuteAsync(null);
        }

        SV.ViewChanged += SV_ViewChanged;
    }

    /// <summary>
    /// 滚动条滚动到极限后
    /// </summary>
    public AsyncRelayCommand AddData
    {
        get; set;
    }

    public RelayCommand<ItemsControl> ScrollLoad
    {
        get; set;
    }
    public ScrollViewer? SV
    {
        get;
        set;
    }


    public async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var page = e.AddedItems[0] as BiliBiliAPI.Models.Videos.Page;
        VI = (await Video.GetVideo(VideoContent, VideoIDType.AV, page.Cid)).Data;
        if(Supports != null)Supports.Clear();
        Supports = VI.Support_Formats.ToObservableCollection();
        RefershSupport();
        var danmu = await Danmaku.GetDanmakuTest(VideoPages[PageSelectIndex].Cid);
        _NowDanmuList = (await Danmaku.GetFormatDanmakuText(danmu)).ToObservableCollection();
        
        
    }

    public SolidColorBrush GetSolidColorBrush(string hex)
    {
        hex = hex.Replace("#", string.Empty);

        bool existAlpha = hex.Length == 8;

        if (!existAlpha && hex.Length != 6)
        {
            throw new ArgumentException("输入的hex不是有效颜色");
        }

        int n = 0;
        byte a;
        if (existAlpha)
        {
            n = 2;
            a = (byte)ConvertHexToByte(hex, 0);
        }
        else
        {
            a = 0xFF;
        }

        var r = (byte)ConvertHexToByte(hex, n);
        var g = (byte)ConvertHexToByte(hex, n + 2);
        var b = (byte)ConvertHexToByte(hex, n + 4);
        return new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
    }

    private static uint ConvertHexToByte(string hex, int n, int count = 2)
    {
        return Convert.ToUInt32(hex.Substring(n, count), 16);
    }

    private void DanmuProcessTime_Tick(object? sender, object e)
    {
        var nowpositon = NowMediaPlayer.PlaybackSession.Position.TotalSeconds;
        var danmakulist =NowDanmuList.Where(p => p.Time > nowpositon && p.Time - nowpositon < 1).ToList();
        foreach (var item in danmakulist)
        {
            var style = new DanmakuTextStyle()
            {
                Size = item.FontSize,
                FontWeight = FontWeights.Bold,
                FontFamily = new Microsoft.UI.Xaml.Media.FontFamily("微软雅黑")
            };
            SolidColorBrush color = new SolidColorBrush();
            var text = "";
            try
            {
                text = "#" + System.Convert.ToString(System.Convert.ToInt32(item.Color.ToString()), 16);
                style.Color = text != "令牌无效" ? GetSolidColorBrush(text) : new SolidColorBrush(Colors.White);
            }
            catch (Exception)
            {
                text = "令牌无效";
            }
            //增加弹幕
            switch (item.DanmakuType)
            {
                case "5":
                    DanmakuControl.CreateTopText(item.Text, style);
                    break;
                case "1":
                    DanmakuControl.CreateScrollText(item.Text, style);
                    break;
                case "2":
                    DanmakuControl.CreateScrollText(item.Text, style);
                    break;
                case "3":
                    DanmakuControl.CreateScrollText(item.Text, style);
                    break;
                case "4":
                    DanmakuControl.CreateBootomText(item.Text, style);
                    break;
            }
        }
    }


    public void Support_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        SelectFormats = (Support_Formats)e.AddedItems[0];
        SetMediaPlayer();
    }

    async void SetMediaPlayer()
    {
        // https://www.microsoft.com/en-us/p/hevc-video-extensions-from-device-manufacturer/9n4wgh0z6vhq
        // HEVC解码器位置
        //https://apps.microsoft.com/store/detail/av1-video-extension/9MVZQVXJBQ9V?hl=zh-cn&gl=cn
        //AV1解码器位置
        foreach (var item in VI.Dash.DashVideos)
        {
            if (item.ID == this.SelectFormats!.Quality)
            {
                // hev 和 avc，使用avc兜底，hev优先
                if (item.Codecs.StartsWith("hev"))
                {
                    Source = await PlayerHelper.CreateMediaSourceAsync(item, VI.Dash.DashAudio[0]);
                    NowMediaPlayer.SetMediaSource(Source!.AdaptiveMediaSource);
                    break;
                }
                if (item.Codecs.StartsWith("avc"))
                {
                    Source = await PlayerHelper.CreateMediaSourceAsync(item, VI.Dash.DashAudio[0]);
                    NowMediaPlayer.SetMediaSource(Source!.AdaptiveMediaSource);
                    break;
                }
            }
        }
    }

    private void NowMediaPlayer_MediaOpened(MediaPlayer sender, object args)
    {
        App.MainWindow.DispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Low, () =>
        {
            DanmuProcessTime = new DispatcherTimer();
            DanmuProcessTime.Interval = new TimeSpan(0, 0, 1);
            DanmuProcessTime.Tick += DanmuProcessTime_Tick;
            DanmuProcessTime.Start();
        });
    }

    public async void RefershSupport()
    {
        switch (await LocalSettingsService.ReadSettingAsync<int>(BiliStart.Models.Settings.Player_Supper_Supper))
        {
            case 0:
                SupportIndex = GetSupportIndex("4K");
                break;
            case 1:
                SupportIndex = GetSupportIndex("1080");
                break;
            case 2:
                SupportIndex = GetSupportIndex("720");
                break;
        }
    }


    
    int GetSupportIndex(string value)
    {
        for (int i = 0; i < Supports.Count; i++)
        {
            switch (value)
            {
                case "4K":
                    if (Supports[i].Quality == "120")
                        return i;
                    else
                        continue;
                case "1080":
                    if (Supports[i].Quality == "80")
                        return i;
                    else
                        continue;
                case "720":
                    if (Supports[i].Quality == "64")
                        return i;
                    else
                        continue;
            }
        }
        return 1;
    }



    public ILocalSettingsService LocalSettingsService
    {
        get;
    }
    public ITipShow TipShow
    {
        get;
    }
}
