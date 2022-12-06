using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiliStart.Contracts.Services;
using Microsoft.UI.Xaml;

namespace BiliStart.Services
{
    public class WindowManager : IWindowManager
    {
        WinUIEx.WindowManager Manager;
        private Window window;
        public Window Window
        {
            get => window??null;
            set
            {
                UnInit();
                window = value;
                Init();
            }
        }
        void Init()
        {
            Manager = WinUIEx.WindowManager.Get(window);
        }

        void UnInit()
        {
            
        }

        public void SetIcon(string icon)
        {
            window.SetIcon(icon);
        }

        public void SetSize(int height, int width)
        {
            Manager.Height= height;
            Manager.Width= width;
        }

        public void SetMinHeightAndWidth(int height, int width)
        {
            Manager.MinHeight= height;
            Manager.MinWidth= width;
        }
    }
}
