using BilibiliAPI;
using BilibiliAPI.Account;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Models.Settings;
using BiliStart.Dialog;
using BiliStart.Event;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZUDesignControl.Controls;

namespace BiliStart.ViewModel
{
    internal class HomeVM: ObservableRecipient, IRecipient<LoginedEvent>,IRecipient<FrameBaseNavigtion>
    {
        Logins Logins = new Logins();

        public AccountToken token = AccountSettings.Read();

        Frame RootFrame { get; set; }
        public HomeVM()
        {
            IsActive = true;
            Loaded = new RelayCommand<NavigationView>((arg) =>
            {
                this._NavigationView = arg;
                loaded();
            });
            GetFrame = new RelayCommand<Frame>((frame) =>
            {
                this.RootFrame = frame;
            });
            LoginOrOpen = new RelayCommand(() =>
            {
                if(IsLogin)
                {

                }
                else
                {
                    LoginDialog dialog = new LoginDialog();
                    dialog.Show();
                }
            });

        }
        bool IsLogin { get; set; }
        async void loaded()
        {
            if (File.Exists(AccountSettings.FilePath))
            {
                token = AccountSettings.Read();
                BiliBiliArgs.TokenSESSDATA = token;
                _LoginResult = (await Logins.Login(token)).Data;
                IsLogin = true;
            }
        }

        /// <summary>
        /// 登陆成功的消息
        /// </summary>
        /// <param name="message"></param>
        public void Receive(LoginedEvent message)
        {
            loaded();
        }

        public ContentControl ContentControl { get; set; }

        public void Receive(FrameBaseNavigtion message)
        {
            //_NavigationView!.PanelTitle = message.Page.Tag.ToString();
            RootFrame.NavigationService.Navigate(message.Page,message.pararm);
        }

        private AccountLoginResultData LoginResult;
        private NavigationView? _NavigationView;

        public AccountLoginResultData _LoginResult
        {
            get { return LoginResult; }
            set => SetProperty(ref LoginResult, value);
        }

        public RelayCommand<NavigationView> Loaded { get;private  set; }
        public RelayCommand<Frame> GetFrame { get; private set; }
        public RelayCommand LoginOrOpen { get; private set; }
    }
}
