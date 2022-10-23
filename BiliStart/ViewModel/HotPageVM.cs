using BilibiliAPI.Video;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public RelayCommand Loaded { get;private  set; }
        private ObservableCollection<HomeVideoVM> Item;
        public ObservableCollection<HomeVideoVM> _Item
        {
            get { return Item; }
            set => SetProperty(ref Item, value);
        }
    }
}
