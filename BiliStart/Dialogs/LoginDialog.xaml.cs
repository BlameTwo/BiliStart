using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using BiliStart.ViewModels;
using BiliStart.ViewModels.DialogViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BiliStart.Dialogs;
public sealed partial class LoginDialog : ContentDialog
{
    public LoginDialogViewModel ViewModel
    {
        get;
    }
    public LoginDialog()
    {
        ViewModel = App.GetService<LoginDialogViewModel>();
        this.InitializeComponent();
        Loaded += LoginDialog_Loaded;
    }

    private void LoginDialog_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.Dialog = this;
    }
}
