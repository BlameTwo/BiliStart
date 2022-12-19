using BiliBiliAPI.Models.HomeVideo;
using BiliStart.Contracts.Services;
using BiliStart.Pages;
using BiliStart.Pages.Dynamics;
using BiliStart.ViewModels;
using BiliStart.ViewModels.PageViewModels;
using BiliStart.ViewModels.PageViewModels.DynamicsViewModels;
using BiliStart.Views;

using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

namespace BiliStart.Services;

public class PageService : IPageService
{
    private readonly Dictionary<string, Type> _pages = new();

    public PageService()
    {
        //注册全局页面信息
        Configure<SettingsViewModel, SettingsPage>();
        Configure<HomeViewModel, HomePage>();
        Configure<PlayerViewModel, PlayerPage>();
        Configure<HotViewModel, HotPage>();
        Configure<MainViewModel,MainPage>();
        Configure<TopMoreViewModel, TopMorePage>();
        Configure<SearchViewModel, SearchPage>();
        Configure<DynamicViewModel,DynamicPage>();

        //排行榜导航页面
        Configure<RankViewModel, RankPage>();
        Configure<WeekViewModel, WeekPage>();

        Configure<MusicAllViewModel, MusicAllPage>();
        Configure<MustWatchViewModel, MustWatchPage>();
        
        Configure<VideoPlayerViewModel, VideoPlayerPage>();

        //动态页面
        Configure<MyInfoViewModel,MyInfoPage>();  

        Configure<PGCPlayerViewModel, PGCPlayerPage>();
    }

    public Type GetPageType(string key)
    {
        Type? pageType;
        lock (_pages)
        {
            if (!_pages.TryGetValue(key, out pageType))
            {
                throw new ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?");
            }
        }

        return pageType;
    }

    //2022.12.19，去除掉VM泛型约束
    public void Configure<VM, V>()
        where V : Page
    {
        lock (_pages)
        {
            var key = typeof(VM).FullName!;
            if (_pages.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in PageService");
            }

            var type = typeof(V);
            if (_pages.Any(p => p.Value == type))
            {
                throw new ArgumentException($"This type is already configured with key {_pages.First(p => p.Value == type).Key}");
            }

            _pages.Add(key, type);
        }
    }
}
