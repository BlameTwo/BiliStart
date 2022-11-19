using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliBiliAPI.Models.Region;
using BiliStart.Contracts.Services;
using BiliStart.Services;
using BiliStart.ViewModels.PageViewModels;
using BiliStart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.ViewModels;
public partial class TopMoreViewModel:ObservableRecipient
{
    BiliBiliAPI.Region.TidRegion TidRegion = new();

    public IHotNavigationService navigationService { get; }
    public IHotNavigationViewService navigationViewService
    {
        get;
    }



    public void NavigationControl_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if(args.SelectedItem is NavigationViewItem item  && item.Tag != null)
        {
            try
            {
                int value = int.Parse(item.Tag.ToString()!);
                navigationService.NavigateTo(typeof(RankViewModel).FullName!,value);
                //分区导航
            }
            catch (Exception)
            {
                //普通导航
            }
        }
    }

    public TopMoreViewModel(IHotNavigationService navigationService, IHotNavigationViewService navigationViewService)
    {
        IsActive = true;
        this.navigationService = navigationService;
        this.navigationViewService = navigationViewService;
        TidData = new ObservableCollection<NavigationViewItem>();
    }

    [RelayCommand]
    public async Task Loaded()
    {
        var result = (await TidRegion.GetTidIcon()).Data.ToObservableCollection();
        for (int i = 0; i < result.Count; i++)
        {
            if (!string.IsNullOrWhiteSpace(result[i].Logo))
            {
                var item = new NavigationViewItem() { Tag = result[i].Tid, Content = result[i].Name };
                item.Icon = new ImageIcon() { Source = new BitmapImage(new Uri(result[i].Logo)) };
                
                TidData.Add(item);
            }
        }
    }




    private ObservableCollection<NavigationViewItem> tiddata;
    public ObservableCollection<NavigationViewItem> TidData
    {
        get => tiddata;
        set=>SetProperty(ref tiddata, value);   
    }
}
