using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.HomeVideo;
using BiliBiliAPI.Video;
using BiliStart.Contracts.Services;
using BiliStart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.ViewModels
{
    public partial class HotViewModel:ScrolViewModelBase
    {
        private BiliBiliAPI.Video.Video _Video = new();
        public HotViewModel()
        {
            _Item = new ObservableCollection<BiliBiliAPI.Models.HomeVideo.Item>();
            AddData = new AsyncRelayCommand(async () => await addata());
            GoVideo = new AsyncRelayCommand<BiliBiliAPI.Models.HomeVideo.Item>(async (arg) => await govideo(arg!));
        }

        async Task govideo(Item arg)
        {
            var result = await _Video.GetVideosContent(arg.PlayArg.Aid, BiliBiliAPI.Models.VideoIDType.AV);
            App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
            {
                var navigationService = App.GetService<IAppNavigationService>();
                navigationService.NavigationTo(AppNavigationViewsEnum.RootFrame, typeof(PlayerViewModel).FullName!, result);
            });
        }

        private async Task addata()
        {
            var items = (await _Video.GetHotVideo(_Item.Last(),Item.Count)).Item.ToObservableCollection() ;
            foreach (var item in items)
            {
                _Item.Add(item);
            }
        }

        [RelayCommand]
        async Task Load()
        {
            var result = (await _Video.GetHotVideo(null,0)).Item.ToObservableCollection();
            foreach (var item in result)
            {
                _Item.Add(item);
            }
        }

        public AsyncRelayCommand<BiliBiliAPI.Models.HomeVideo.Item> GoVideo
        {
            get; private set;
        }

        private ObservableCollection<BiliBiliAPI.Models.HomeVideo.Item> Item;

        public ObservableCollection<BiliBiliAPI.Models.HomeVideo.Item> _Item
        {
            get => Item;
            set => SetProperty(ref Item, value);
        }


    }
}
