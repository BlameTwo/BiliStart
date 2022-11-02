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
    public class SearchAnimationVM:ObservableRecipient
    {
        PublicSearch Search = new PublicSearch();
        public SearchAnimationVM(string SearchKey)
        {
            IsActive = true;
            Loaded = new RelayCommand(() => load());
            this.SearchKey = SearchKey;
        }

        async void load()
        {
            _Item = (await Search.SearchAnimation(SearchKey,Index)).Data.Items.ToObservableCollection();
        }
        int Index = 1;
        public RelayCommand Loaded { get; private set; }
        public string SearchKey { get; }


        private ObservableCollection<AniationItem> Item;

        public ObservableCollection<AniationItem> _Item
        {
            get { return Item; }
            set => SetProperty(ref Item, value);
        }


    }
}
