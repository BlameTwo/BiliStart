using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Search;
using Microsoft.UI.Xaml;

namespace BiliStart.ViewModels.SearchModels;
public class SearchMovieViewModel : SearchViewModelBase
{
    public SearchMovieViewModel()
    {
        Changed = new Action<string>((str) => OnChanged(str));
        Changing = new Action<string>((str) => OnChanging(str));
        Popup_Visibility = Visibility.Collapsed;
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

    private Visibility visibility;
    public Visibility Popup_Visibility
    {
        get => visibility; set=>SetProperty(ref visibility, value);
    }

    private string tipmessage;
    public string TipMessage
    {
        get => tipmessage; set=> SetProperty(ref tipmessage, value);
    }


    private async  void OnChanged(string str)
    {
        var result = await Search.SearchMovie(str, 1);
        if (result.Data.Items == null|| result.Data.Items.Count == 0)
        {
            Popup_Visibility = Visibility.Visible;
            TipMessage = "什么都没有找到捏";
        }
        else
        {
            Items = result.Data.Items.ToObservableCollection();
        }
    }
}
