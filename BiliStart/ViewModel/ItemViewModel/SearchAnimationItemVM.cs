using BiliBiliAPI.Models.Search;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BiliStart.ViewModel.ItemViewModel
{
    public class SearchAnimationItemVM:ObservableObject
    {

        public SearchAnimationItemVM()
        {
            SelectIndex = new RelayCommand<Episodes>((arg) => selection(arg));
        }

        private void selection(Episodes? arg)
        {
            MessageBox.Show($"选择了第{arg.Index}集，epid为{arg.Uriparam}");
        }

        private Aniation_Movie_Item Item;

        public Aniation_Movie_Item _Item
        {
            get { return Item; }
            set=>SetProperty(ref Item, value);
        }


        public RelayCommand<Episodes> SelectIndex { get; private set; }
    }
}
