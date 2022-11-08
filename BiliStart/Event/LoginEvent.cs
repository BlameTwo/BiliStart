using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.Event;
public class LoginEvent
{
    public LoginEventEnum Event
    {
        get;set;    
    }

    public string Message
    {
        get; set;
    }
}

public enum LoginEventEnum
{
    Login,
    UnLogin
}
