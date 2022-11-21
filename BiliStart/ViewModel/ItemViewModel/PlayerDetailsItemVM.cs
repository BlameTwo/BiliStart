using BiliBiliAPI.Video;
using BiliBiliAPI.Models.Videos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.ViewModel.ItemViewModel
{
    public class PlayerDetailsItemVM:ObservableRecipient
    {
        public PlayerDetailsItemVM()
        {
            Loaded = new RelayCommand(() => load());
        }

        async void load()
        {
            Desc = (await video.GetVideoDesc(VC.Aid, BiliBiliAPI.Models.VideoIDType.AV)).Data;
        }

        Video video = new Video();

        private VideosContent vc;

        public VideosContent VC
        {
            get { return vc; }
            set 
            {
                SetProperty(ref vc, value);
            }  
        }

        private string  desc;

        public string  Desc
        {
            get { return desc; }
            set=>SetProperty(ref desc, value);
        }
        public RelayCommand Loaded { get; private set; }

    }
}
