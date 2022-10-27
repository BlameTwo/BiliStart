using BilibiliAPI;
using BiliBiliAPI.Models.TopList;
using BiliBiliAPI.Models.Videos;
using BiliStart.Converter;
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
            Search = new RelayCommand<CidModel>((arg) => search(arg));
            _Cids = new ObservableCollection<CidModel>();
            _Cids.Add(new CidModel() { cid = Cid.All});
            _Cids.Add(new CidModel() { cid = Cid.Douga });
            _Cids.Add(new CidModel() { cid = Cid.Chinese});
            _Cids.Add(new CidModel() { cid = Cid.Music});
            _Cids.Add(new CidModel() { cid = Cid.Dance });
            _Cids.Add(new CidModel() { cid = Cid.Game });
            _Cids.Add(new CidModel() { cid = Cid.knowledge });
            _Cids.Add(new CidModel() { cid = Cid.Tech });
            _Cids.Add(new CidModel() { cid = Cid.Sports });
            _Cids.Add(new CidModel() { cid = Cid.Car  });
            _Cids.Add(new CidModel() { cid = Cid.Life  });
            _Cids.Add(new CidModel() { cid = Cid.Food  });
            _Cids.Add(new CidModel() { cid = Cid.Animal  });
            _Cids.Add(new CidModel() { cid = Cid.Kichiku  });
            _Cids.Add(new CidModel() { cid = Cid.Fashion  });
            _Cids.Add(new CidModel() { cid = Cid.Ent  });
            _Cids.Add(new CidModel() { cid = Cid.Documentary  });
            _Cids.Add(new CidModel() { cid = Cid.Movie  });
            _Cids.Add(new CidModel() { cid = Cid.TV  });

        }

        private async void search(CidModel? arg)
        {
            var list = await TopListVideo.GetTopVideo(arg!.cid, 7);
            List = list.Data.List.ToObservableCollection();
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

        private ObservableCollection<CidModel> Cids;

        public ObservableCollection<CidModel> _Cids
        {
            get { return Cids; }
            set => SetProperty(ref Cids, value);
        }



        public RelayCommand Loaded { get; private set; }
        public RelayCommand<TopVideo> Selected { get; set; }
        public RelayCommand<CidModel> Search { get; private set; }
    }

    public class CidModel
    {
        CidFormat Cid = new CidFormat();
        public string DisplayName
        {
            get
            {
                return Cid.FromEnum(cid);
            }
        }

        public Cid cid
        {
            get;set;    
        }
    }
}
