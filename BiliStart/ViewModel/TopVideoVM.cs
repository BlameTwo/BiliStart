using BiliBiliAPI.Models.TopList;
using BiliBiliAPI.Models.Videos;
using BiliStart.Windows;
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
    internal class TopVideoVM:ObservableRecipient
    {
        public TopVideoVM()
        {
            IsActive = true;
            Loaded = new RelayCommand(() => loaded());
            Selected = new RelayCommand<TopVideo>((arg) => selected(arg));
        }

        private void selected(TopVideo? arg)
        {
            var value = (arg as VideosContent)!;
            PlayerWindows playerWindows = new PlayerWindows(value);
            playerWindows.Show();
        }

        BilibiliAPI.TopVideos.TopListVideo TopListVideo = new BilibiliAPI.TopVideos.TopListVideo(); 
        private async void loaded()
        {
            List = (await TopListVideo.GetTopVideo(BilibiliAPI.Cid.All,7)).Data.List.ToObservableCollection();
        }

        private ObservableCollection<TopVideo> _List;

        public ObservableCollection<TopVideo> List
        {
            get { return _List; }
            set=>SetProperty(ref _List, value);
        }

        public RelayCommand Loaded { get; private set; }
        public RelayCommand<TopVideo> Selected { get; set; }
    }
}
