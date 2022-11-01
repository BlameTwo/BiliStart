using BilibiliAPI.Search;
using BilibiliAPI.Video;
using BiliBiliAPI.Models.Search;
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
    public class SearchVideoVM:ObservableRecipient
    {
        PublicSearch Search = new PublicSearch();

        public SearchVideoVM(string searchKey)
        {
            IsActive = true;
            Loaded = new RelayCommand(() =>
            {
                loaded();
            });

            AddData = new RelayCommand(async() =>
            {
                var list = (await Search.GetVideo(SearchKey, Index, OrderBy.Default, 1)).Data.Items.ToObservableCollection();
                for (int i = 0; i < list.Count; i++)
                {
                    MyList.Add(list[i]);
                }
                if (list.Count > 0) Index++;
            });

            Selected = new RelayCommand<Item>((arg) => selected(arg));
            SearchKey = searchKey;
        }
        Video video = new Video();
        private async void selected(Item? arg)
        {
            if(arg != null)
            {
                if(arg.Goto == "av")
                {
                    var content = await video.GetVideosContent(arg.LinkParam, BiliBiliAPI.Models.VideoIDType.AV);
                    PlayerWindows play = new PlayerWindows(content.Data);
                    play.Show();
                }
            }
        }

        private async void loaded()
        {
            MyList = (await Search.GetVideo(SearchKey, Index, OrderBy.Default, 1)).Data.Items.ToObservableCollection(); ;
        }
        int Index { get; set; } = 1;
        private ObservableCollection<BiliBiliAPI.Models.Search.Item> List;

        public ObservableCollection<BiliBiliAPI.Models.Search.Item> MyList
        {
            get { return List; }
            set=>SetProperty(ref List,value);
        }

        public RelayCommand<Item> Selected { get; private set; }

        public RelayCommand AddData { get; private set; }

        public string SearchKey { get; set; }

        public RelayCommand Loaded { get; private set; }
    }
}
