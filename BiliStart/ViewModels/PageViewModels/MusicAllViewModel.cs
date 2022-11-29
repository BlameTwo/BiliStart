using System.Collections.ObjectModel;
using BiliBiliAPI.Models.HomeVideo;
using BiliBiliAPI.Models.TopList;
using BiliStart.Contracts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.ViewModels.PageViewModels;
public partial class MusicAllViewModel:ObservableRecipient
{
    BiliBiliAPI.TopLists.MusicRank Rank = new();
    BiliBiliAPI.Video.Video Video = new();
    public MusicAllViewModel(IGoVideo goVideo)
    {
        Year = new();
        _Musics = new();
        GoVideo = goVideo;
    }

    MusicRankList Source = new();



    [RelayCommand]
    public async void Loaded()
    {
        var result = await Rank.GetRankList();
        Source = result.Data;
        foreach (var item in Source.YearData)
        {
            Year.Add(int.Parse(item.Year));
        }
    }

    public async void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            var data = (MusicRankData)e.AddedItems[0];
            BiliStart.ViewModels.Models.PlayerArgs arg = new()
            {
                Aid = long.Parse(data.CreateAid ?? data.MVBvid),
                Bvid = data.CreateBvid??data.MVBvid,
                Content = (await Video.GetVideosContent(data.CreateAid??data.MVAid, BiliBiliAPI.Models.VideoIDType.AV)).Data
            };
            GoVideo.PlayerArgs = arg;
            GoVideo.Go();
        }
    }

    private ObservableCollection<MusicRankListItem> musicRank;
    public ObservableCollection<MusicRankListItem> MusicRank
    {
        get => musicRank;
        set=>SetProperty(ref musicRank, value);
    }


    public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            foreach (var item in Source.YearData)
            {
                if(item.Year == e.AddedItems[0].ToString())
                {
                    MusicRank = item.MusicRankItem.ToObservableCollection();
                    return;
                }
            }
        }
        catch (Exception)
        {
            //选中错误
        }
    }

    private ObservableCollection<MusicRankData> Musics;

    public ObservableCollection<MusicRankData> _Musics
    {
        get => Musics;
        set => SetProperty(ref Musics, value);
    }


    public async void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var value = (e.AddedItems[0] as MusicRankListItem)!.ID;
        var result = await Rank.GetMusics(value);
        _Musics = result.Data.Items.ToObservableCollection();
    }

    private ObservableCollection<int> year;
    public ObservableCollection<int> Year
    {
        get => year;
        set=>SetProperty(ref year, value);
    }
    public IGoVideo GoVideo
    {
        get;
    }
}
