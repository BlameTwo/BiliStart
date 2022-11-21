using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Search;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ViewModels;
public class SearchViewModelBase:ObservableRecipient
    
{

    public Action<string> Changing;
    public Action<string> Changed;

    public PublicSearch Search = new();
    public SearchViewModelBase()
    {

    }

    private string SearchKey;

    public string _SearchKey
    {
        get => SearchKey;
        set
        {
            if (!EqualityComparer<string?>.Default.Equals(SearchKey, value))
            {
                OnSearchChanging(value);
                OnPropertyChanging();
                SearchKey = value;
                OnSearchChanged(SearchKey);
                OnPropertyChanged();
            }
        }
    }

    private void OnSearchChanging(string value)
    {
        Changing.Invoke(value);
    }
    private void OnSearchChanged(string value)
    {
        Changed.Invoke(value);
    }
}
