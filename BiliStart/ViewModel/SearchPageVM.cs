using BiliBiliAPI.Search;
using BiliBiliAPI.Models.Search;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZUDesignControl.Controls;
using RadioButton = ZUDesignControl.Controls.RadioButton;

namespace BiliStart.ViewModel
{
    internal class SearchPageVM:ObservableRecipient
    {
        PublicSearch Search = new PublicSearch();
        ContentControl Content { get; set; }
        public SearchPageVM()
        {
            IsActive = true;
            Loaded = new RelayCommand<ContentControl>((arg)=>load(arg!));
            Checked = new RelayCommand<string>((arg) => check(arg!));
        }

        private void check(string radioButton)
        {
            switch (radioButton)
            {
                case "视频":
                    Content.Content =new BiliStart.Controls.SearchPivotItems.SearchVideo() { SearchKey = _SearchKey};
                    break;
                case "动画":
                    Content.Content = new BiliStart.Controls.SearchPivotItems.SearchAnimation() { SearchKey = _SearchKey};  
                    break;
                case "电影":
                    Content.Content = new BiliStart.Controls.SearchPivotItems.SearchMovie() { SearchKey = _SearchKey };
                    break;
                case "专栏":
                    break;
                default:
                    break;
            }
        }

        async void load(ContentControl arg)
        {
            Content = arg;
        }

        public RelayCommand<ContentControl> Loaded { get; private set; }
        public RelayCommand<string> Checked { get; private set; }

        


        private string SearchKey;

        public string _SearchKey
        {
            get { return SearchKey; }
            set=>SetProperty(ref SearchKey, value);
        }



    }
}
