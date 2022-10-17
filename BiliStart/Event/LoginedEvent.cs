using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.Event
{
    public class LoginedEvent
    {
        /// <summary>
        /// 1为关闭，0为不关闭
        /// </summary>
        public LoginState Event { get; set; }
    }


    public enum LoginState
    {
        Login,UnLogin
    }
    
}
