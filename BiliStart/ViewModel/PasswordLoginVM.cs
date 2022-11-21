using BiliBiliAPI;
using BiliBiliAPI.Account;
using BiliBiliAPI.User;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Models.Settings;
using BiliStart.Event;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BiliStart.ViewModel
{
    public class PasswordLoginVM: ObservableRecipient
    {

        private WebView2 webview2;
        AccountPasswordLogin passlogin = new AccountPasswordLogin();

        public PasswordLoginVM()
        {
            IsActive = true;
            Login = new RelayCommand(async () =>
            {
                var result = await passlogin.LoginV3(_User, _Password);

                if (result.Data.message == "本次登录环境存在风险, 需使用手机号进行验证或绑定")
                {
                    webview2!.Source = new Uri(result.Data.GoUrl);
                    webview2.Visibility = Visibility.Visible;
                }
            });

            WebViewLoaded = new RelayCommand<WebView2>((arg) =>
            {
                webview2 = arg!;
                webview2.NavigationCompleted += WebView2_NavigationCompleted;
                webview2.NavigationStarting += WebView2_NavigationStarting;
            });
        }

        private void WebView2_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            var c = BiliBiliAPI.ApiTools.Tools.GetFormData(e.Uri);
            if (c == null) return;
            if (c.ContainsKey("access_key"))
            {
                AccountToken token = new AccountToken();
                string mid = "";
                string token2 = "";
                c.TryGetValue("mid", out mid!);
                c.TryGetValue("access_key", out token2!);
                token.Mid = mid; token.SECCDATA = token2;
                BiliBiliArgs.TokenSESSDATA = token;
                AccountSettings.Write(BiliBiliArgs.TokenSESSDATA);
                WeakReferenceMessenger.Default.Send(new LoginedEvent() { Event = LoginState.Login });
            }
        }

        private async void WebView2_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if ((sender as WebView2)!.Source.Host == "www.bilibili.com")
            {
                webview2.Visibility = Visibility.Collapsed;
                (sender as WebView2)!.CoreWebView2.Navigate("https://passport.bilibili.com/login/app/third?appkey=27eb53fc9058f8c3&api=http%3A%2F%2Flink.acg.tv%2Fforum.php&sign=67ec798004373253d60114caaad89a8c");
            }
            if ((sender as WebView2)!.Source.ToString() == "https://passport.bilibili.com/login/app/third?appkey=27eb53fc9058f8c3&api=http%3A%2F%2Flink.acg.tv%2Fforum.php&sign=67ec798004373253d60114caaad89a8c")
            {
                try
                {
                    object obj = await (sender as WebView2)!.CoreWebView2.ExecuteScriptAsync("document.body.innerText");
                    string result = "";
                    string result2 = obj.ToString()!.Replace(@"\", "").Substring(1);
                    string result3 = result2.Substring(0, result2.Length - 1);
                    string a = JObject.Parse(result3)["data"]!["confirm_uri"]!.ToString();
                    (sender as WebView2)!.CoreWebView2.Navigate(a);
                    var jj = await (sender as WebView2)!.CoreWebView2.CookieManager.GetCookiesAsync((sender as WebView2)!.CoreWebView2.Source.ToString());
                }
                catch (Exception)
                {

                }
            }
        }

        private string User;

        public string _User
        {
            get { return User; }
            set { User = value; }
        }


        private string  Password;

        public string  _Password
        {
            get { return Password; }
            set { Password = value; }
        }

        public RelayCommand<WebView2> WebViewLoaded { get; private set; }
        public RelayCommand Login { get; private set; }
    }
}
