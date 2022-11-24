using BiliStart.Helpers;
using Microsoft.UI.Xaml;
using System.Runtime.InteropServices; // For DllImport
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()
using WinUIEx;
namespace BiliStart;


class WindowsSystemDispatcherQueueHelper
{
    [StructLayout(LayoutKind.Sequential)]
    struct DispatcherQueueOptions
    {
        internal int dwSize;
        internal int threadType;
        internal int apartmentType;
    }

    [DllImport("CoreMessaging.dll")]
    private static unsafe extern int CreateDispatcherQueueController(DispatcherQueueOptions options, IntPtr* instance);

    IntPtr m_dispatcherQueueController = IntPtr.Zero;
    public void EnsureWindowsSystemDispatcherQueueController()
    {
        if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
        {
            // one already exists, so we'll just use it.
            return;
        }

        if (m_dispatcherQueueController == IntPtr.Zero)
        {
            DispatcherQueueOptions options;
            options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
            options.threadType = 2;    // DQTYPE_THREAD_CURRENT
            options.apartmentType = 2; // DQTAT_COM_STA

            unsafe
            {
                IntPtr dispatcherQueueController;
                CreateDispatcherQueueController(options, &dispatcherQueueController);
                m_dispatcherQueueController = dispatcherQueueController;
            }
        }
    }
}

public sealed partial class MainWindow : Window
{
    WindowsSystemDispatcherQueueHelper m_wsdqHelper;
    BackdropType m_currentBackdrop;
    Microsoft.UI.Composition.SystemBackdrops.MicaController m_micaController;
    Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController m_acrylicController;
    Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration m_configurationSource;

    public enum BackdropType
    {
        Mica,
        MicaAlt,
        DesktopAcrylic,
        DefaultColor,
    }

    public MainWindow()
    {
        InitializeComponent();
        this.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/icon.ico"));
        //使用WindowManager进行窗口管理
        WindowManager manager = WindowManager.Get(this);
        m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
        m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();
        SetBackdrop(BackdropType.DesktopAcrylic);
        Title = "AppDisplayName".GetLocalized();
        manager.MinWidth = 650;
        manager.MinHeight = 500;
        int build = Environment.OSVersion.Version.Build;
        if (build >= 22000 && build < 22621)
        {
            SetBackdrop(BackdropType.Mica);
        }
        else if(build == 22621)
        {
            SetBackdrop(BackdropType.MicaAlt);
        }
        else if(build < 22000){
            SetBackdrop(BackdropType.DesktopAcrylic);
        }
        else
        {
            SetBackdrop(BackdropType.DefaultColor);
        }
    }


    public void SetBackdrop(BackdropType type)
    {
        m_currentBackdrop = BackdropType.DefaultColor;
        if (m_micaController != null)
        {
            m_micaController.Dispose();
            m_micaController = null;
        }
        if (m_acrylicController != null)
        {
            m_acrylicController.Dispose();
            m_acrylicController = null;
        }
        this.Activated -= Window_Activated;
        this.Closed -= Window_Closed;
        
        m_configurationSource = null;

        if (type == BackdropType.Mica)
        {
            if (TrySetMicaBackdrop(false))
            {
                m_currentBackdrop = type;
            }
            else
            {
                // Mica isn't supported. Try Acrylic.
                type = BackdropType.DesktopAcrylic;
            }
        }
        if (type == BackdropType.MicaAlt)
        {
            if (TrySetMicaBackdrop(true))
            {
                m_currentBackdrop = type;
            }
            else
            {
                // MicaAlt isn't supported. Try Acrylic.
                type = BackdropType.DesktopAcrylic;
            }
        }
        if (type == BackdropType.DesktopAcrylic)
        {
            if (TrySetAcrylicBackdrop())
            {
                m_currentBackdrop = type;
            }
            else
            {
            }
        }
    }

    bool TrySetMicaBackdrop(bool useMicaAlt)
    {
        if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
        {
            // Hooking up the policy object
            m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
            this.Activated += Window_Activated;
            this.Closed += Window_Closed;

            // Initial configuration state.
            m_configurationSource.IsInputActive = true;

            m_micaController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();

            if (useMicaAlt)
            {
                m_micaController.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt;
            }
            else
            {
                m_micaController.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;
            }

            m_micaController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
            m_micaController.SetSystemBackdropConfiguration(m_configurationSource);
            return true; // succeeded
        }

        return false; // Mica is not supported on this system
    }

    bool TrySetAcrylicBackdrop()
    {
        if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
        {
            // Hooking up the policy object
            m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
            this.Activated += Window_Activated;
            this.Closed += Window_Closed;

            // Initial configuration state.
            m_configurationSource.IsInputActive = true;

            m_acrylicController = new Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController();

            // Enable the system backdrop.
            // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            m_acrylicController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
            m_acrylicController.SetSystemBackdropConfiguration(m_configurationSource);
            return true; // succeeded
        }

        return false; // Acrylic is not supported on this system
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args)
    {
        m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
    }

    private void Window_Closed(object sender, WindowEventArgs args)
    {
        // Make sure any Mica/Acrylic controller is disposed so it doesn't try to
        // use this closed window.
        if (m_micaController != null)
        {
            m_micaController.Dispose();
            m_micaController = null;
        }
        if (m_acrylicController != null)
        {
            m_acrylicController.Dispose();
            m_acrylicController = null;
        }
        this.Activated -= Window_Activated;
        m_configurationSource = null;
    }



}
