using BiliStart.Helpers;

namespace BiliStart;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();


        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/icon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();
    }

}
