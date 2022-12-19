using System.Collections.ObjectModel;
using BiliBiliAPI.Models.Search;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;

namespace BiliStart.ViewModels.SearchModels;
public class SearchMovieViewModel : SearchViewModelBase
{
    public SearchMovieViewModel()
    {
        Changed = new Action<string>((str) => OnChanged(str));
        Changing = new Action<string>((str) => OnChanging(str));
        Popup_Visibility = Visibility.Collapsed;
        AddData = new CommunityToolkit.Mvvm.Input.AsyncRelayCommand(async () => await adddata());
    }
    int Index =1;
    private async Task adddata()
    {
        var result = await Search.SearchMovie(this._SearchKey, Index); 
        if (result.Data.Items == null) return;
        foreach (var item in result.Data.Items)
        {
            Items.Add(item);
        }
        Index++;
    }
    string Key;
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
        if (result.Data == null) return;
        if (result.Data.Items == null|| result.Data.Items.Count == 0)
        {
            Popup_Visibility = Visibility.Visible;
            TipMessage = "什么都没有找到捏";
        }
        else
        {
            Items = result.Data.Items.ToObservableCollection();
            Index++;
        }
    }
}
