using BilibiliAPI.Video;
using BiliStart.Event;
using BiliStart.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.ViewModel
{
    internal class HotPageVM:ObservableRecipient
    {
        public HotPageVM()
        {
            IsActive = true;
            _Item = new ObservableCollection<HomeVideoVM>();
            Loaded = new RelayCommand(() =>load());
            AddData = new RelayCommand(() => adddate());
            SelectChanged = new RelayCommand<HomeVideoVM>((arg) => selected(arg));
        }
        Video video = new Video();

        //推荐页和热门页都为一个VM
        private async void load()
        {
            var items = (await video.GetHotVideo()).Item.ToObservableCollection();
            foreach (var item in items)
            {
                _Item.Add(new HomeVideoVM() { _Item = item });
            }
        }

        private async void selected(HomeVideoVM? arg)
        {
            var value = await video.GetVideosContent(arg!._Item.PlayArg.Aid, BiliBiliAPI.Models.VideoIDType.AV);
            PlayerPage page = new PlayerPage(value.Data!) { Tag = arg._Item.Title };
            WeakReferenceMessenger.Default.Send(new FrameBaseNavigtion() { Event = NavigtionEvent.Navigation, Page = page });
        }

        private async void adddate()
        {
            var items = (await video.GetHomeVideo()).Data.Item.ToObservableCollection();
            foreach (var item in items)
            {
                _Item.Add(new HomeVideoVM() { _Item = item });
            }
        }

        public RelayCommand Loaded { get;private  set; }

        public RelayCommand<HomeVideoVM> SelectChanged { get; private set; }
        public RelayCommand AddData { get; private set; }
        private ObservableCollection<HomeVideoVM> Item;
        public ObservableCollection<HomeVideoVM> _Item
        {
            get { return Item; }
            set => SetProperty(ref Item, value);
        }
    }
}
