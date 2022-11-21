using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Search;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ItemsViewModel;
[ObservableObject]
public partial class SearchMovieItemViewModel
{
    private Aniation_Movie_Item Data;

    public Aniation_Movie_Item _Data
    {
        get => Data;
        set=>SetProperty(ref Data, value);
    }

}