using BiliBiliAPI.Search;

namespace BiliStart.ViewModels;

public partial class SearchViewModelBase : ScrolViewModelBase
{
    public Action<string> Changed
    {
        get;set;
    }

    public PublicSearch Search = new();

    private string SearchKey;


    public string _SearchKey
    {
        get => SearchKey;
        set
        {
            if (!EqualityComparer<string?>.Default.Equals(SearchKey, value))
            {
                OnPropertyChanging();
                SearchKey = value;
                OnSearchChanged(SearchKey);
                OnPropertyChanged();
            }
        }
    }


    private void OnSearchChanged(string value)
    {
        Changed.Invoke(value);
    }
}