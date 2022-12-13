using System.Collections.ObjectModel;
using BiliBiliAPI.Models.HomeVideo;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels;

public partial class HomeViewModel: ScrolViewModelBase
{
    BiliBiliAPI.WebHome.WebHomeData Video = new ();
    BiliBiliAPI.Video.Video videocontent = new();

    public HomeViewModel(IGoVideo goVideo)
    {
        IsActive = true;
        _Data = new();
        AddData = new AsyncRelayCommand(async() => await adddata());
        Loaded = new AsyncRelayCommand(async () => await load());
        GoVideo = new AsyncRelayCommand<BiliBiliAPI.Models.HomeVideo.HomeDataItem>(async (arg) => await govideo(arg!));
        GoVideo1 = goVideo;
    }
    async Task govideo(BiliBiliAPI.Models.HomeVideo.HomeDataItem arg)
    {
        BiliStart.ViewModels.Models.PlayerArgs arg2 = new BiliStart.ViewModels.Models.PlayerArgs()
        {
            Aid = long.Parse(arg.Aid)
        };
        var result = (await videocontent.GetVideosContent(arg.Aid, BiliBiliAPI.Models.VideoIDType.AV)).Data;
        arg2.Content = result;
        GoVideo1.PlayerArgs = arg2;
        GoVideo1.Go();
    }
    async Task load()
    {
        for (int i = 0; i < 2; i++)
        {
            var result = (await Video.GetWebHomeVideo(30));
            if (result != null)
            {
                foreach (var item in result.Data.Items)
                {
                    _Data.Add(item);
                }
            }
        }
    }

    [RelayCommand]
    public async Task Refresh()
    {
        _Data.Clear();
        _Data = (await Video.GetWebHomeVideo(30)).Data.Items.ToObservableCollection();
    }

    private async Task adddata()
    {
        await load();

    }

    public AsyncRelayCommand<BiliBiliAPI.Models.HomeVideo.HomeDataItem> GoVideo
    {
        get;private  set;    
    }


    private ObservableCollection<BiliBiliAPI.Models.HomeVideo.HomeDataItem> Data;

    public ObservableCollection<BiliBiliAPI.Models.HomeVideo.HomeDataItem> _Data
    {
        get => Data;
        set => SetProperty(ref Data, value);
    }

    public AsyncRelayCommand Loaded
    {
        get;private set;
    }
    public IGoVideo GoVideo1
    {
        get;
    }
}
