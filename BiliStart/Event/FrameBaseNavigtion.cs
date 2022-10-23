using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BiliStart.Event
{
    public class FrameBaseNavigtion
    {
        public UserControl Page { get; set; }
        public NavigtionEvent Event { get; set; }
    }

    public enum NavigtionEvent
    {
        Navigation
    }
}
