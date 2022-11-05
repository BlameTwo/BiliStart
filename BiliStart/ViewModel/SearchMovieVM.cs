using BilibiliAPI.Search;
using BiliStart.ViewModel.ItemViewModel;
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
    internal class SearchMovieVM:ObservableRecipient
    {
        public SearchMovieVM(string SearchKey)
        {
            IsActive = true;
            this.SearchKey = SearchKey;
            _Items = new ObservableCollection<SearchAnimationItemVM>();
            Loaded = new RelayCommand(() => loaded());
        }
        PublicSearch Search = new PublicSearch();
        private async void loaded()
        {
            var list = (await Search.SearchMovie(SearchKey,Index)).Data.Items.ToObservableCollection();
            foreach (var item in list)
            {
                _Items.Add(new SearchAnimationItemVM() { _Item = item });
            }
        }
        int Index;
        private ObservableCollection<SearchAnimationItemVM> Items;

        public ObservableCollection<SearchAnimationItemVM> _Items
        {
            get { return Items; }
            set=>SetProperty(ref Items, value);
        }

        public string SearchKey { get; }

        public RelayCommand Loaded { get; private set; }
    }
}
