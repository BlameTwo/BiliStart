using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BiliBiliAPI.Models.HomeVideo;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BiliStart.ViewModels;
public class HomeViewModel:ObservableRecipient
{
    BilibiliAPI.Video.Video Video = new ();

    public HomeViewModel()
    {
        IsActive = true;
        _Data = new ObservableCollection<Item>();
        Loaded = new AsyncRelayCommand(async()=>await load());
    }


    async Task load()
    {
        _Data = (await Video.GetHomeVideo()).Data.Item.ToObservableCollection();
        
    }

    private ObservableCollection<Item> Data;

    public ObservableCollection<Item> _Data
    {
        get => Data;
        set => SetProperty(ref Data, value);
    }

    public AsyncRelayCommand Loaded
    {
        get;private set;
    }

}
