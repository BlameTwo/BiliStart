using BiliBiliAPI.Models.TopList;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BiliStart.ItemsViewModel;

[ObservableObject]
public partial class MusicRankItemViewModel
{
    private MusicRankData Data;

    public MusicRankData _Data
    {
        get => Data;
        set=>SetProperty(ref Data, value);
    }

}
