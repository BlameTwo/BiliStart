using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace BiliStart.Contracts.Services
{
    /// <summary>
    /// 窗口管理
    /// </summary>
    public interface IWindowManager
    {
        Window Window
        {
            get;set;
        }
        void SetSize(int height,int width);
        void SetIcon(string icon);

        void SetMinHeightAndWidth(int height,int width);

        void SetTitle(string title);
    }
}
