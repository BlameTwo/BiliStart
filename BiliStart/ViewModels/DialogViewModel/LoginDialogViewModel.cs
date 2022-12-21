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

    /// <summary>
    /// 验证码定时器
    /// </summary>
    DispatcherTimer SendTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

    /// <summary>
    /// 发送验证码间隔为60秒
    /// </summary>
    readonly int SendNumber = 60;

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
                SendTimer.Stop();
                break;
            case "Phone":
                CounityItems = (await papi.GetCounityList()).Data.Lists.ToObservableCollection();
                timer.Stop();
                SendTimer.Start();
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

    [ObservableProperty]
    string _SMSTip;

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

    [RelayCommand]
    public async void SetCid(CounityItem item)
    {
        this.Cid = item;
    }


    [RelayCommand()]
    public async void PhoneLogin()
    {
        if(Key != null)
        {//Code=86202:验证码错误，Code=0登录成功
            var result =await  papi.PostSMSPoll(Key,Capkey,Phone,Cid.Code);
            if(result.Code == "86202")
            {
                SMSTip = result.Message;
            }
            if(result.Code == "0")
            {
                BiliBiliArgs.TokenSESSDATA = result.Data;
                BiliBiliArgs.TokenSESSDATA.LoginType = 1;
                BiliBiliArgs.TokenSESSDATA.SECCDATA = result.Data.Info.SECCDATA;
                BiliBiliArgs.TokenSESSDATA.RefToken = result.Data.Info.RefToken;
                BiliBiliArgs.TokenSESSDATA.Expires_in = result.Data.Info.Expires_in;
                BiliBiliArgs.TokenSESSDATA.Mid = result.Data.Info.Mid;
                AccountSettings.Write(BiliBiliArgs.TokenSESSDATA);
                timer.Stop();
                timer.Tick -= Timer_Tick;
                App.IsLogin = true;
                WeakReferenceMessenger.Default.Send<LoginEvent>(new LoginEvent() { Event = LoginEventEnum.Login, Message = "用户登录" });
                Dialog.Hide();
            }
        }
    }

    CounityItem Cid =null;
    string Capkey = "";
    [RelayCommand()]
    public async void SendSMS()
    {
        if (this.Cid == null)
        {
            SMSTip = "国家地区不正确";
        }
        else
        {
            var result =  await papi.PostSMSSend(this.Cid.Code, Phone);
            if(result.Data.Captcha_Key != null)
            {
                SMSTip = "验证码发送成功！";
                Capkey = result.Data.Captcha_Key;
                return;
            }
            SMSTip = "验证码发送失败！";
        }
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
