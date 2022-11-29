using System.Collections.ObjectModel;
using BiliBiliAPI.Models.TopList;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.ViewModels.PageViewModels;
public partial class MusicAllViewModel:ObservableRecipient
{
    BiliBiliAPI.TopLists.MusicRank Rank = new();
    
    public MusicAllViewModel()
    {
        Year = new();
        _Musics = new();
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



}
