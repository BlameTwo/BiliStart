using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Search;

namespace BiliStart.ViewModels.SearchModels;
public class SearchMovieViewModel : SearchViewModelBase
{
    public SearchMovieViewModel()
    {
        Changed = new Action<string>((str) => OnChanged(str));
        Changing = new Action<string>((str) => OnChanging(str));

    }

    private void OnChanging(string str)
    {

    }

    private ObservableCollection<Aniation_Movie_Item> Item;

    public ObservableCollection<Aniation_Movie_Item> Items
    {
        get => Item;
        set=>SetProperty(ref Item, value);
    }


    private async  void OnChanged(string str)
    {
        var result = await Search.SearchMovie(str, 1);
        Items = result.Data.Items.ToObservableCollection();
    }
}
