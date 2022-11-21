using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BiliStart.ViewModel.HotViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BiliBiliAPI;
using BiliBiliAPI.TopLists;
using BiliBiliAPI.Video;
using BiliBiliAPI.Models.TopList;

namespace BiliStart.ViewModel;
public class EveryoneWeekPageVM:ObservableRecipient
{
    EveryoneWeek EveryWeek = new EveryoneWeek();
    public EveryoneWeekPageVM()
    {
        _Items = new ObservableCollection<HotWeakDTVM>();
        Loaded = new AsyncRelayCommand(load);
        WeekSelectionChanged = new AsyncRelayCommand<EveryoneWeekData>(async (arg) =>
        {
            var result = (await EveryWeek.GetWeekTopList(arg!.Number));
            _Items.Clear();
            foreach (var item in result.Data.Items.ToObservableCollection())
            {
                _Items.Add(new HotWeakDTVM() { _Items = item });
            }
        });

        PlayerSelectionChanged = new AsyncRelayCommand<HotWeakDTVM>(async (arg) =>
        {
            if (arg == null) return;
            Video video = new Video();
            var result =   await video.GetVideosContent(arg._Items.Aid, BiliBiliAPI.Models.VideoIDType.AV);
            Windows.PlayerWindows win = new Windows.PlayerWindows(result.Data);
            win.Show();
        });
    }

    async Task load()
    {
        _EVeryList = (await EveryWeek.GetWeekList()).Data.List.ToObservableCollection();
    }

    private ObservableCollection<HotWeakDTVM> Item;

    public ObservableCollection<HotWeakDTVM> _Items
    {
        get => Item;
        set => SetProperty(ref Item, value);
    }


    private ObservableCollection<EveryoneWeekData> EveryList;

    public ObservableCollection<EveryoneWeekData> _EVeryList
    {
        get
        {
            return EveryList;
        }
        set => SetProperty(ref EveryList, value);
    }

    public AsyncRelayCommand Loaded
    {
        get;private set;
    }
    public AsyncRelayCommand<EveryoneWeekData> WeekSelectionChanged
    {
        get; private set;
    }

    public AsyncRelayCommand<HotWeakDTVM> PlayerSelectionChanged
    {
        get;private set;
    }



}
