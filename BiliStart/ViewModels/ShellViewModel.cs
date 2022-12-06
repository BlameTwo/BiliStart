using System.Collections.ObjectModel;
using BiliBiliAPI;
using BiliBiliAPI.Account;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Search;
using BiliStart.Contracts.Services;
using BiliStart.Dialogs;
using BiliStart.Event;
using BiliStart.Views;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.ViewModels;

public partial class ShellViewModel : ObservableRecipient, IRecipient<LoginEvent>
{
    private bool _isBackEnabled;
    private object? _selected;
    private DefaultSearch DefaultSearch = new DefaultSearch();

    SearchSquare SearchSquare = new();

    public IAppNavigationService NavigationService
    {
        get;
    }
    public IPageService PageService
    {
        get;
    }
    public IAppNavigationViewService NavigationViewService
    {
        get;
    }



    public ITipShow TipShow
    {
        get;
    }
    public AsyncRelayCommand<Flyout> Loaded
    {
        get;private set;
    }
    public bool IsBackEnabled
    {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public object? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    Logins Login = new Logins();



    public ShellViewModel(IAppNavigationService navigationService, IPageService pageService, IAppNavigationViewService navigationViewService,ITipShow tipShow )
    {
        IsActive = true;
        if(App.Token != null)
        {
            BiliBiliArgs.TokenSESSDATA = App.Token;
        }
        else
        {
            BiliBiliArgs.TokenSESSDATA = new AccountToken();
        }
        NavigationService = navigationService;
        PageService = pageService;
        NavigationViewService = navigationViewService;
        NavigationService.ShellNavigated += OnNavigated;
        NavigationViewService = navigationViewService;
        NavigationService = navigationService;
        TipShow = tipShow;
        Loaded = new AsyncRelayCommand<Flyout>(async(arg)=> await load(arg!));
        UserClick = new AsyncRelayCommand(async () =>
        {
            flyout!.Hide();
            if (!App.IsLogin)
            {
                LoginDialog loginDialog = new LoginDialog();
                loginDialog.XamlRoot = (App.MainWindow.Content as MainPage)!.XamlRoot;
                await loginDialog.ShowAsync();
            }
            else
            {
                _login();
                flyout.ShowAt(FlyoutButton);
            }
        });
        UnLogin = new RelayCommand(()=>unlogin());

    }



    private ObservableCollection<string> SearchItems;

    public ObservableCollection<string> _SearchItems
    {
        get => SearchItems;
        set => SetProperty(ref SearchItems, value);
    }


    private string PlaceholderTextSearch;

    public string _PlaceholderTextSearch
    {
        get => PlaceholderTextSearch;
        set => SetProperty(ref PlaceholderTextSearch, value);
    }


    private void unlogin()
    {
        _LoginData = new AccountLoginResultData() { Name = "账号未登录", Face_Image = "https://i0.hdslb.com/bfs/face/member/noface.jpg@240w_240h_1c_1s.webp" };
        App.IsLogin = false;
        Login.Unlogin();
        flyout.Hide();
        TipShow.SendMessage("用户退出登录", "账户操作");
    }

    public Button FlyoutButton;

    Flyout flyout;

    private async Task load(Flyout flyout)
    {
        this.flyout = flyout ?? null;
        var result = (await Login.Login(App.Token));
        if (result.Code == "-101")
        {
            _LoginData = new AccountLoginResultData() { Name = "账号未登录", Face_Image = "https://i0.hdslb.com/bfs/face/member/noface.jpg@240w_240h_1c_1s.webp", Exp = new Level_Exp() };
        }
        else if (result.Data != null)
        {
            _LoginData = result.Data;
            App.IsLogin = true;
        }
        await InitSearch();

    }

    async void _login()
    {
        var result = (await Login.Login(App.Token));
        _LoginData = result.Data;
    }



    public async void UpDataList(string text)
    {
        var result = await SearchSquare.GetSearchSuggest(text);
        if (result == null) return;
        _SearchItems.Clear();
        if(result.Result != null &&  result.Result.Values != null)
        {
            foreach (var item in result.Result.Values)
            {
                _SearchItems.Add(item.Value.ToString());
            }
        }
        else
        {
            await RefershSearchHotRank();
        }
    }


    public void Search(string key)
    {
        if (!string.IsNullOrWhiteSpace(key))
            NavigationService.NavigationTo( AppNavigationViewsEnum.ShellFrame,typeof(SearchViewModel).FullName!, key);
        else
        {
            if (!string.IsNullOrWhiteSpace(_PlaceholderTextSearch))
            {
                NavigationService.NavigationTo(AppNavigationViewsEnum.ShellFrame, typeof(SearchViewModel).FullName!, _PlaceholderTextSearch);
            }
        }
    }

    public async Task InitSearch()
    {
        var value = (await DefaultSearch.GetDefault());
        if(value.Data != null)
        {
            _PlaceholderTextSearch = value.Data.Title;
        }
        await RefershSearchHotRank();
    }


    public async Task RefershSearchHotRank()
    {
        var result = (await SearchSquare.GetSearchHotRank(10));
        _SearchItems = new();
        foreach (var item in result.Data.Trending.List)
        {
            _SearchItems.Add(item.ShowName);
        }
    }

    public AsyncRelayCommand UserClick
    {
        get;set;    
    }

    private AccountLoginResultData LoginData;

    public AccountLoginResultData _LoginData
    {
        get => LoginData;
        set=> SetProperty(ref LoginData, value);
    }



    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = (bool)NavigationService.CanShellFrameBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem( AppNavigationViewsEnum.ShellFrame,e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }

    

    public RelayCommand UnLogin
    {
        get;private set;
    }

    public void Receive(LoginEvent message)
    {
        switch (message.Event)
        {
            case LoginEventEnum.Login:
                _login();

                TipShow.SendMessage("用户登录", "账户操作");
                break;
            case LoginEventEnum.UnLogin:
                break;
        }
    }
}
