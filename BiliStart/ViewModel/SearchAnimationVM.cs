using BiliBiliAPI.Search;
using BiliBiliAPI.Models.Search;
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
    public class SearchAnimationVM:ObservableRecipient
    {
        PublicSearch Search = new PublicSearch();
        public SearchAnimationVM(string SearchKey)
        {
            IsActive = true;
            Loaded = new RelayCommand(() => load());
            this.SearchKey = SearchKey;
            Items = new ObservableCollection<SearchAnimationItemVM>();
        }

        async void load()
        {
            var value = (await Search.SearchAnimation(SearchKey,Index)).Data.Items.ToObservableCollection();
            foreach (var item in value)
            {
                Items.Add(new SearchAnimationItemVM() { _Item = item });
            }
        }
        int Index = 1;
        public RelayCommand Loaded { get; private set; }
        public string SearchKey { get; }


        private ObservableCollection<SearchAnimationItemVM> items;

        public ObservableCollection<SearchAnimationItemVM> Items
        {
            get { return items; }
            set => SetProperty(ref items, value);
        }


    }
}
