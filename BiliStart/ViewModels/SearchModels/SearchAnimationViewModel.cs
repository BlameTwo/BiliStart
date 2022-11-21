using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Search;

namespace BiliStart.ViewModels.SearchModels;
public class SearchAnimationViewModel:SearchViewModelBase
{
    public SearchAnimationViewModel()
    {
        Changed = new Action<string>((str) => OnChanged(str));
        Changing = new Action<string>((str) => OnChanging(str));
    }

    private async void OnChanged(string str)
    {
        var result = await Search.SearchAnimation(str,1);
        _Items = result.Data.Items.ToObservableCollection();
    }

    private ObservableCollection<Aniation_Movie_Item> Items;
    public ObservableCollection<Aniation_Movie_Item> _Items
    {
        get => Items;
        set=>SetProperty(ref Items, value);
    }

    private void OnChanging(string str)
    {
    
    }
}
