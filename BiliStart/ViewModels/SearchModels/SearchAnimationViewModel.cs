using System.Collections.ObjectModel;
using BiliBiliAPI.Models.Search;
using Microsoft.UI.Xaml;

namespace BiliStart.ViewModels.SearchModels;
public class SearchAnimationViewModel : SearchViewModelBase
{
    public SearchAnimationViewModel()
    {
        Changed = new Action<string>((str) => OnChanged(str));
        Changing = new Action<string>((str) => OnChanging(str));

        Popup_Visibility = Visibility.Collapsed;
    }

    private async void OnChanged(string str)
    {
        var result = await Search.SearchAnimation(str, 1);
        if (result.Data.Items == null || result.Data.Items.Count == 0)
        {
            Popup_Visibility = Visibility.Visible;
            TipMessage = "或许换一个搜索关键字？";
        }
        else
        {
            _Items = result.Data.Items.ToObservableCollection();
        }
    }

    private ObservableCollection<Aniation_Movie_Item> Items;
    public ObservableCollection<Aniation_Movie_Item> _Items
    {
        get => Items;
        set=>SetProperty(ref Items, value);
    }

    private Visibility visibility;
    public Visibility Popup_Visibility
    {
        get => visibility; set => SetProperty(ref visibility, value);
    }

    private string tipmessage;
    public string TipMessage
    {
        get => tipmessage; set => SetProperty(ref tipmessage, value);
    }

    private void OnChanging(string str)
    {
    
    }
}
