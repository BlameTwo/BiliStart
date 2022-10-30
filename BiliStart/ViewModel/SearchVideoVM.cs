using BilibiliAPI.Search;
using BiliBiliAPI.Models.Search;
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
            SearchKey = searchKey;
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

        public string SearchKey { get; set; }

        public RelayCommand Loaded { get; private set; }
    }
}
