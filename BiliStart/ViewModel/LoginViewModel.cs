using BilibiliAPI;
using BilibiliAPI.Account;
using BiliBiliAPI.Models;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Models.Settings;
using BiliStart.Event;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ZUDesignControl.Controls;

namespace BiliStart.ViewModel
{
    public class LoginViewModel : ObservableRecipient, IRecipient<LoginedEvent>
    {
        public DialogHost dialoghost { get; set; }
        AccountQRLogin QRLogin = new AccountQRLogin();
        public LoginViewModel()
        {
            IsActive = true;

            
        }

        public void Receive(LoginedEvent message)
        {
            if(message.Event ==  LoginState.Login)
            {
                dialoghost.Close();
            }
        }
    }
}
