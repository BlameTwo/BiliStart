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

namespace BiliStart.ViewModel
{
    internal class HomeVM: ObservableRecipient, IRecipient<LoginedEvent>
    {
        Logins Logins = new Logins();

        public AccountToken token = AccountSettings.Read();
        public HomeVM()
        {
            IsActive = true;
            Loaded = new RelayCommand(() =>
            {
                loaded();
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

        private AccountLoginResultData LoginResult;

        public AccountLoginResultData _LoginResult
        {
            get { return LoginResult; }
            set => SetProperty(ref LoginResult, value);
        }

        public RelayCommand Loaded { get;private  set; }
        public RelayCommand LoginOrOpen { get; private set; }
    }
}
