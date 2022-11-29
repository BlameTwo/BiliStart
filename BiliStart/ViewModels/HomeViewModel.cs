using System.Collections.ObjectModel;
using BiliBiliAPI.Models.HomeVideo;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;

namespace BiliStart.ViewModels;

public partial class HomeViewModel: ScrolViewModelBase
{
    BiliBiliAPI.Video.Video Video = new ();

    public HomeViewModel(IGoVideo goVideo)
    {
        IsActive = true;
        _Data = new ObservableCollection<Item>();
        AddData = new AsyncRelayCommand(async() => await adddata());
        Loaded = new AsyncRelayCommand(async () => await load());
        GoVideo = new AsyncRelayCommand<Item>(async (arg) => await govideo(arg!));
        GoVideo1 = goVideo;
    }
    async Task govideo(Item arg)
    {
        BiliStart.ViewModels.Models.PlayerArgs arg2 = new BiliStart.ViewModels.Models.PlayerArgs()
        {
            Aid = long.Parse(arg.PlayArg.Aid),
            Type = Models.GoToType.Video
        };
        var result = (await Video.GetVideosContent(arg.PlayArg.Aid, BiliBiliAPI.Models.VideoIDType.AV)).Data;
        arg2.Content = result;
        GoVideo1.PlayerArgs = arg2;
        GoVideo1.Go();
    }
    async Task load()
    {
        for (int i = 0; i < 2; i++)
        {
            var result = (await Video.GetHomeVideo());
            if (result != null)
            {
                foreach (var item in result.Data.Item)
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
        _Data = (await Video.GetHomeVideo()).Data.Item.ToObservableCollection();
    }

    private async Task adddata()
    {
        await load();

    }

    public AsyncRelayCommand<Item> GoVideo
    {
        get;private  set;    
    }


    private ObservableCollection<Item> Data;

    public ObservableCollection<Item> _Data
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
