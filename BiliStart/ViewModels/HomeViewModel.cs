using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BiliBiliAPI.Models.HomeVideo;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI.WebUI;

namespace BiliStart.ViewModels;
public partial class HomeViewModel: ScrolViewModelBase
{
    BiliBiliAPI.Video.Video Video = new ();

    public HomeViewModel()
    {
        IsActive = true;
        _Data = new ObservableCollection<Item>();
        AddData = new AsyncRelayCommand(async() => await adddata());
        Loaded = new AsyncRelayCommand(async () => await load());
        GoVideo = new AsyncRelayCommand<Item>(async (arg) => await govideo(arg!));
    }
    async Task govideo(Item arg)
    {
        var result = await Video.GetVideosContent(arg.PlayArg.Aid, BiliBiliAPI.Models.VideoIDType.AV);
        // Queue navigation with low priority to allow the UI to initialize.
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            var navigationService = App.GetService<INavigationService>();
            navigationService.NavigateTo(typeof(PlayerViewModel).FullName!,result);
        });
    }
    async Task load()
    {
        for (int i = 0; i < 2; i++)
        {
            var result = (await Video.GetHomeVideo()).Data.Item.ToObservableCollection();
            foreach (var item in result)
            {
                _Data.Add(item);
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

}
