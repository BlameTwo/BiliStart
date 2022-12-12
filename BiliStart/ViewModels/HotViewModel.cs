using System.Collections.ObjectModel;
using BiliBiliAPI.Models.HomeVideo;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels
{
    public partial class HotViewModel:ScrolViewModelBase
    {
        private BiliBiliAPI.Video.Video _Video = new();
        public HotViewModel(IGoVideo goVideo)
        {
            _Item = new ObservableCollection<BiliBiliAPI.Models.HomeVideo.Item>();
            AddData = new AsyncRelayCommand(async () => await addata());
            GoVideo = new AsyncRelayCommand<BiliBiliAPI.Models.HomeVideo.Item>(async (arg) => await govideo(arg!));
            GoVideo1 = goVideo;
        }

        async Task govideo(Item arg)
        {
            BiliStart.ViewModels.Models.PlayerArgs arg2 = new BiliStart.ViewModels.Models.PlayerArgs()
            {
                Aid = long.Parse(arg.PlayArg.Aid)
            };
            var result = (await _Video.GetVideosContent(arg.PlayArg.Aid, BiliBiliAPI.Models.VideoIDType.AV)).Data;
            arg2.Content = result;
            GoVideo1.PlayerArgs = arg2;
            GoVideo1.Go();
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
        public IGoVideo GoVideo1
        {
            get;
        }
    }
}
