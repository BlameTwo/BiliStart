using BiliBiliAPI.Video;
using BiliStart.Event;
using BiliStart.Pages;
using BiliStart.Windows;
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
            GoTopList = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send<FrameBaseNavigtion>(new FrameBaseNavigtion()
                {
                    Event = NavigtionEvent.Navigation,
                    Page = new TopVideoPage(),
                    pararm = "",
                    Tag = "排行榜"
                });
            });

            GoWeek = new RelayCommand(() =>
            {
                WeakReferenceMessenger.Default.Send(new FrameBaseNavigtion()
                {
                    Event = NavigtionEvent.Navigation,
                    Page = new Pages.EveryoneWeekPage(),
                    pararm = "",
                    Tag="每周必看"
                });
            });
        }
        Video video = new Video();

        //推荐页和热门页都为一个VM
        private async void load()
        {
            var items = (await video.GetHotVideo(null,0));
            foreach (var item in items.Item.ToObservableCollection())
            {
                _Item.Add(new HomeVideoVM() { _Item = item });
            }
        }

        private async void selected(HomeVideoVM? arg)
        {
            var value = await video.GetVideosContent(arg!._Item.PlayArg.Aid, BiliBiliAPI.Models.VideoIDType.AV);
            PlayerWindows page = new PlayerWindows(item:value.Data) { Title = value.Data.Bvid };
            page.Show();
        }

        private async void adddate()
        {
            var items = (await video.GetHotVideo(_Item.Last()._Item, _Item.Count)).Item.ToObservableCollection();
            foreach (var item in items)
            {
                _Item.Add(new HomeVideoVM() { _Item = item });
            }
        }

        public RelayCommand Loaded { get;private  set; }

        public RelayCommand<HomeVideoVM> SelectChanged { get; private set; }
        public RelayCommand AddData { get; private set; }
        public RelayCommand GoTopList
        {
            get;private set;
        }
        public RelayCommand GoWeek
        {
            get;private set;    
        }
        private ObservableCollection<HomeVideoVM> Item;
        public ObservableCollection<HomeVideoVM> _Item
        {
            get { return Item; }
            set => SetProperty(ref Item, value);
        }
    }
}
