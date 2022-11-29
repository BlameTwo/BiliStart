using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ViewModels;


public class SearchViewModel:ObservableRecipient
{
    public SearchViewModel(ITipShow tipshow)
    {
        IsActive= true;
        Tipshow = tipshow;
    }

    public ITipShow Tipshow
    {
        get;
    }

    private string SearchKey;

    public string _SearchKey
    {
        get => SearchKey;
        set => SetProperty(ref SearchKey, value); 
    }

}
