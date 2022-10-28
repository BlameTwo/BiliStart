using BilibiliAPI.Search;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.ViewModel
{
    internal class SearchPageVM:ObservableRecipient
    {
        PublicSearch Search = new PublicSearch();
        public SearchPageVM()
        {
            IsActive = true;
            Loaded = new RelayCommand(load);
        }

        async void load()
        {
            var value = await Search.SearchVideo("崩坏世界的歌姬", "5");
        }

        public RelayCommand Loaded { get; private set; }
    }
}
