using BiliBiliAPI.Account;
using BiliBiliAPI.Models.Account;
using BiliBiliAPI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Threading;
using BiliBiliAPI.Models.Settings;
using BiliBiliAPI;
using System.Diagnostics;
using ZUDesignControl.Controls;
using CommunityToolkit.Mvvm.Messaging;
using BiliStart.Event;

namespace BiliStart.ViewModel
{
    public class QRLoginVM: ObservableRecipient, IRecipient<LoginedEvent>
    {
        DispatcherTimer time = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
        AccountQRLogin QRLogin = new AccountQRLogin();
        public QRLoginVM()
        {
            IsActive = true;

            Loaded = new RelayCommand(() =>
            {
            });

            RefererQR = new RelayCommand(() =>
            {
                RefQR();
            });
        }
        private ResultCode<AccountLoginData> QrData;

        public ResultCode<AccountLoginData> _QrData
        {
            get { return QrData; }
            set => SetProperty(ref QrData, value);
        }
        async void RefQR()
        {
            _QrData = await QRLogin.GetQR();
            time.Tick += Time_Tick;
            time.Start();
        }

        private async void Time_Tick(object? sender, EventArgs e)
        {
            var result = await QRLogin.PollQRAuthInfo();
            switch (result.Check)
            {
                case Checkenum.OnTime:
                    Debug.WriteLine("二维码已经失效");
                    break;
                case Checkenum.NULL:
                    Debug.WriteLine("未收录的code状态");
                    break;
                case Checkenum.Yes:
                    Debug.WriteLine($"登录成功！携带的返回值为:\n{result.Body}");
                    time.Stop();
                    var result2 = WebFormat.GoToken(result.Body);
                    //写入到本地保存
                    AccountSettings.Write(result2);
                    BiliBiliArgs.TokenSESSDATA = result2;
                    if(time != null)
                    {
                        time.Stop();
                        time.Tick -= Time_Tick;
                        time = null;
                    }
                    WeakReferenceMessenger.Default.Send(new LoginedEvent() { Event = LoginState.Login });
                    break;
                case Checkenum.No:
                    Debug.WriteLine($"二维码尚未确认");
                    break;
            }
        }

        public void Receive(LoginedEvent message)
        {
            switch (message.Event)
            {
                case LoginState.Login:
                    if(time != null)
                    {
                        time.Stop();
                        time.Tick -= Time_Tick;
                        time = null;
                    }
                    break;
                case LoginState.UnLogin:
                    break;
                default:
                    break;
            }
        }

        public RelayCommand Loaded { get; private set; }
        public RelayCommand RefererQR { get; private set; }
    }

  
}
