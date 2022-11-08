using BilibiliAPI;
using BilibiliAPI.Account;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Models.Settings;
using BiliStart.Contracts.Services;
using BiliStart.Dialogs;
using BiliStart.Event;
using BiliStart.Views;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace BiliStart.ViewModels;

public class ShellViewModel : ObservableRecipient, IRecipient<LoginEvent>
{
    private bool _isBackEnabled;
    private object? _selected;

    public INavigationService NavigationService
    {
        get;
    }



    public INavigationViewService NavigationViewService
    {
        get;
    }
    public AsyncRelayCommand Loaded
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



    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService)
    {
        if(App.Token != null)
        {
            BiliBiliArgs.TokenSESSDATA = App.Token;
        }
        else
        {
            BiliBiliArgs.TokenSESSDATA = new AccountToken();
        }
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        Loaded = new AsyncRelayCommand(async => load());
        UserClick = new AsyncRelayCommand(async () =>
        {
            if (!App.IsLogin)
            {
                LoginDialog loginDialog = new LoginDialog();
                loginDialog.XamlRoot = (App.MainWindow.Content as ShellPage)!.Content.XamlRoot;
                await loginDialog.ShowAsync();
            }
        }); 
    }

    private async Task load()
    {
        var result = (await Login.Login(App.Token));
        if (result.Code == "-101")
        {
            _LoginData = new AccountLoginResultData() { Name = "账号未登录", Face_Image= "https://i0.hdslb.com/bfs/face/member/noface.jpg@240w_240h_1c_1s.webp" };
        }
        else if(result.Data != null)
        {
            _LoginData = result.Data;
        }
    }

    async void _login()
    {

        var result = (await Login.Login(App.Token));
        _LoginData = result.Data;
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
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage))
        {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null)
        {
            Selected = selectedItem;
        }
    }

    public void Receive(LoginEvent message)
    {
        switch (message.Event)
        {
            case LoginEventEnum.Login:
                _login();
                break;
            case LoginEventEnum.UnLogin:
                break;
        }
    }
}
