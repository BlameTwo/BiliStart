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
        public string Tag{get;set; }
        public Page Page { get; set; }
        public object pararm { get; set; }
        public NavigtionEvent Event { get; set; }
    }

    public enum NavigtionEvent
    {
        Navigation
    }
}
