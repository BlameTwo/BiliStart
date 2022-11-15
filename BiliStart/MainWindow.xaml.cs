using BiliStart.Helpers;
using Microsoft.UI.Xaml;
using System.Runtime.InteropServices; // For DllImport
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()


namespace BiliStart;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();
        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/icon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();
        Backdrop =new MicaSystemBackdrop();
    }


}
