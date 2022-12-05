using System.Diagnostics;
using BiliBiliAPI;
using BiliBiliAPI.Account;
using BiliBiliAPI.Models.Account;
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
    }

    AccountQRLogin api = new AccountQRLogin();
    private bool PrimaryEnable;

    public bool _PrimaryEnable
    {
        get
        {
            return PrimaryEnable;
        }
        set
        {
            //为True说明为账号登陆状态
            if (value == false)
            {
                timer.Start();
                if (webview2 != null)
                    webview2.Visibility = Visibility.Collapsed;
            }
            else
            {
                //这里如果未登录就跳转到前一个页面在跳回来会触发，导致无法输入用户密码。
                timer.Stop();
            }
            SetProperty(ref PrimaryEnable, value);
        }
    }

    public void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch ((e.AddedItems[0] as PivotItem).Header.ToString())
        {
            case "扫码登录":
                _PrimaryEnable = false;
                timer.Start();
                break;
            case "账号密码":
                _PrimaryEnable = true;
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

    public async void LoginClick()
    {
        if (_PrimaryEnable)
        {
            AccountPasswordLogin passlogin = new AccountPasswordLogin();
            var result = await passlogin.LoginV3(_User, _Password);
            if (result.Data.message == "本次登录环境存在风险, 需使用手机号进行验证或绑定")
            {
                webview2.Source = new Uri(result.Data.GoUrl);
                webview2.Visibility = Visibility.Visible;
            }
        }
    }
    public async void WebView2_NavigationStarting(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs args)
    {
        

        var c = GetFormData(args.Uri);
        if (c == null) return;
        if (c.ContainsKey("access_key"))
        {
            var str = await sender.CoreWebView2.CookieManager.GetCookiesAsync(null);
            AccountTokenCookies cookie = new AccountTokenCookies();
            cookie.Cookies = new List<Cookie>();
            foreach (var item in str)
            {
                cookie.Cookies.Add(new Cookie()
                {
                    Expires = item.Expires.ToString(),
                    Http_Only = item.IsHttpOnly.ToString()
                    , Name = item.Name,
                    Value = item.Value
                });
            }
            AccountToken token = new AccountToken();
            token.cookies = cookie;
            var mid = "";
            var token2 = "";
            c.TryGetValue("mid", out mid!);
            c.TryGetValue("access_key", out token2!);
            token.Mid = mid; token.SECCDATA = token2;
            BiliBiliArgs.TokenSESSDATA = token;
            AccountSettings.Write(BiliBiliArgs.TokenSESSDATA);
            WeakReferenceMessenger.Default.Send<LoginEvent>(new LoginEvent() { Event = LoginEventEnum.Login, Message = "用户登录"});
            App.IsLogin = true;
            Dialog.Hide();

        }
    }

    /// <summary>
    /// 将获取的formData存入字典数组
    /// </summary>
    public static Dictionary<String, String> GetFormData(string formData)
    {
        try
        {
            string str = formData.Substring(formData.IndexOf('?') + 1);
            //将参数存入字符数组
            String[] dataArry = str.Split('&');

            //定义字典,将参数按照键值对存入字典中
            Dictionary<String, String> dataDic = new Dictionary<string, string>();
            //遍历字符数组
            for (int i = 0; i <= dataArry.Length - 1; i++)
            {
                string dataParm = dataArry[i];
                int dIndex = dataParm.IndexOf("=");
                string key = dataParm.Substring(0, dIndex);
                string value = dataParm.Substring(dIndex + 1, dataParm.Length - dIndex - 1);
                string deValue = System.Web.HttpUtility.UrlDecode(value, System.Text.Encoding.GetEncoding("utf-8"));
                //将参数以键值对存入字典
                dataDic.Add(key, deValue);
            }
            return dataDic;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    WebView2 webview2 = new();
    public void WebViewLoad(object sender, RoutedEventArgs e)
    {
        webview2 = (sender as WebView2)!;
        webview2.NavigationCompleted += WebView2_NavigationCompleted;
        webview2.NavigationStarting += WebView2_NavigationStarting;
    }

    public async void WebView2_NavigationCompleted(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
    {
        if (sender.Source.Host == "www.bilibili.com")
        {
            webview2.Visibility = Visibility.Collapsed;
            sender.CoreWebView2.Navigate("https://passport.bilibili.com/login/app/third?appkey=27eb53fc9058f8c3&api=http%3A%2F%2Flink.acg.tv%2Fforum.php&sign=67ec798004373253d60114caaad89a8c");
        }
        if (sender.Source.ToString() == "https://passport.bilibili.com/login/app/third?appkey=27eb53fc9058f8c3&api=http%3A%2F%2Flink.acg.tv%2Fforum.php&sign=67ec798004373253d60114caaad89a8c")
        {
            try
            {
                object obj = await sender.CoreWebView2.ExecuteScriptAsync("document.body.innerText");
                var result = "";
                var result2 = obj.ToString()!.Replace(@"\", "").Substring(1);
                var result3 = result2.Substring(0, result2.Length - 1);
                var a = JObject.Parse(result3)["data"]!["confirm_uri"]!.ToString();
                sender.CoreWebView2.Navigate(a);
                var jj = await sender.CoreWebView2.CookieManager.GetCookiesAsync(sender.CoreWebView2.Source.ToString());
            }
            catch (Exception)
            {

            }
        }
    }

    public void LoginClose()
    {
        timer.Tick -= Timer_Tick;
        timer.Stop();
        timer = null;
        webview2.Close();
        webview2 = null;
        api = null;
        GC.SuppressFinalize(this);
        GC.Collect();
    }

    private string Password;

    public string _Password
    {
        get
        {
            return Password;
        }
        set => SetProperty(ref Password, value);
    }

}
