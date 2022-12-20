using System.Collections.ObjectModel;
using System.Diagnostics;
using BiliBiliAPI;
using BiliBiliAPI.Account;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Models.Account.PhoneLoginModel;
using BiliBiliAPI.Models.Settings;
using BiliStart.Behaviors.Converter;
using BiliStart.Dialogs;
using BiliStart.Event;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Newtonsoft.Json.Linq;

namespace BiliStart.ViewModels.DialogViewModel;
public partial class LoginDialogViewModel : ObservableRecipient
{
    /// <summary>
    /// 定时器一秒
    /// </summary>
    DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
    public LoginDialogViewModel()
    {
        IsActive = true;
        Key = "";
        Phone = "";
    }

    AccountQRLogin api = new AccountQRLogin();
    PhoneLogin papi = new();

    [ObservableProperty]
    ObservableCollection<CounityItem> _CounityItems;


    public async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch ((e.AddedItems[0] as PivotItem).Tag.ToString())
        {
            case "QR":
                timer.Start();
                break;
            case "Phone":
                CounityItems = (await papi.GetCounityList()).Data.Lists.ToObservableCollection();
                timer.Stop();
                break;
        }
    }

    private AccountLoginData QR;

    public AccountLoginData _QR
    {
        get
        {
            return QR;
        }
        set => SetProperty(ref QR, value);
    }

    [ObservableProperty]
    string _Key;

    [ObservableProperty]
    string _Phone;

    private BitmapImage QRImage;

    public BitmapImage _QRImage
    {
        get => QRImage;
        set =>SetProperty(ref QRImage, value);
    }

    [RelayCommand]
    public async void RefQr()
    {
        _QR = (await api.GetQR()).Data;
        _QRImage = await QRConvert.Convert(_QR.PicUrl);
        timer.Tick += Timer_Tick;
        timer.Start();
    }



    [RelayCommand()]
    public async void PhoneLogin()
    {
    
    }


    [RelayCommand()]
    public async void SendSMS()
    {
        
    }

    private string User;

    public string _User
    {
        get => User;
        set => SetProperty(ref User, value);
    }

    public LoginDialog Dialog
    {
        get;set;    
    }

    private async void Timer_Tick(object sender, object e)
    {
        var result = await api.PollQRAuthInfo();
        switch (result.Check)
        {
            case Checkenum.OnTime:
                Debug.WriteLine("二维码已经失效");
                break;
            case Checkenum.NULL:
                Debug.WriteLine("未收录的code状态");
                break;
            case Checkenum.Yes:
                Debug.WriteLine($"登录成功！返回值未:\n{result.Body}");
                BiliBiliArgs.TokenSESSDATA = WebFormat.GoToken(result.Body);
                BiliBiliArgs.TokenSESSDATA.LoginType = 0;
                AccountSettings.Write(BiliBiliArgs.TokenSESSDATA);
                timer.Stop();
                timer.Tick -= Timer_Tick;
                App.IsLogin = true;
                WeakReferenceMessenger.Default.Send<LoginEvent>(new LoginEvent() { Event = LoginEventEnum.Login, Message = "用户登录" });
                Dialog.Hide();
                break;
            case Checkenum.No:
                Debug.WriteLine("二维码未确认");
                break;
        }
    }

    

    public void LoginClose()
    {
        timer.Tick -= Timer_Tick;
        timer.Stop();
        timer = null;
        api = null;
        GC.SuppressFinalize(this);
        GC.Collect();
    }

}
