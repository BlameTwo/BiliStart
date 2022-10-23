using BilibiliAPI.Video;
using BiliBiliAPI.Models.HomeVideo;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using BiliStart;
using CommunityToolkit.Mvvm.Input;
using System.Security.Policy;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using BiliStart.Event;
using CommunityToolkit.Mvvm.Messaging;
using BiliStart.Pages;

namespace BiliStart.ViewModel
{
    public class RecommendVM :ObservableRecipient
    {
        BilibiliAPI.Video.Video video = new BilibiliAPI.Video.Video();
        public RecommendVM()
        {
            IsActive = true;
            _Item = new ObservableCollection<HomeVideoVM>();
            Loaded = new RelayCommand(update);
            AddData = new RelayCommand(adddate);
            SelectChanged = new RelayCommand<HomeVideoVM>((arg) => selected(arg));
        }

        private async void selected(HomeVideoVM? arg)
        {
            var value =  await video.GetVideosContent(arg!._Item.PlayArg.Aid, BiliBiliAPI.Models.VideoIDType.AV);
            PlayerPage page = new PlayerPage(value.Data!) { Tag = arg._Item.Title};
            WeakReferenceMessenger.Default.Send(new FrameBaseNavigtion() {  Event = NavigtionEvent.Navigation, Page = page });
        }

        private async void adddate()
        {
            var items = (await video.GetHomeVideo()).Data.Item.ToObservableCollection();
            foreach (var item in items)
            {
                _Item.Add(new HomeVideoVM() { _Item = item});
            }
        }

        async void update()
        {
            var value = (await video.GetHomeVideo()).Data.Item.ToObservableCollection();
            foreach (var item in value)
            {
                _Item.Add(new HomeVideoVM() { _Item = item });
            }
        }

        private ObservableCollection<HomeVideoVM> Item;
        public ObservableCollection<HomeVideoVM> _Item
        {
            get { return Item; } 
            set=>SetProperty(ref Item, value);
        }


        public RelayCommand<HomeVideoVM> SelectChanged { get; private set; }
        public RelayCommand AddData { get; private set; }
        public RelayCommand Loaded { get; private set; }
    }
}
