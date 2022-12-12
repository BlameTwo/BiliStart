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
        AddData = new CommunityToolkit.Mvvm.Input.AsyncRelayCommand(async () => await adddata());
        Popup_Visibility = Visibility.Collapsed;
    }
    string Key;
    private async Task adddata()
    {
        var result = await Search.SearchAnimation(Key, Index);
        if (result.Data.Items == null) return;
        foreach (var item in result.Data.Items)
        {
            _Items.Add(item);
        }
        Index++;
    }
    int Index = 1;
    private async void OnChanged(string str)
    {
        this.Key = str;
        var result = await Search.SearchAnimation(str, Index);
        
        if (result.Data.Items == null || result.Data.Items.Count == 0)
        {
            Popup_Visibility = Visibility.Visible;
            TipMessage = "或许换一个搜索关键字？";
        }
        else
        {
            _Items = result.Data.Items.ToObservableCollection();
            Index++;
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
