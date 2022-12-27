// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace BiliStart.Styles.UserControls;
public sealed partial class RootFrameTitleBar : UserControl
{
    public RootFrameTitleBar()
    {
        this.InitializeComponent();
        Loaded += RootFrameTitleBar_Loaded;
        SizeChanged += RootFrameTitleBar_SizeChanged;
    }

    private void RootFrameTitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        SetTitleBar(true);
    }

    private void RootFrameTitleBar_Loaded(object sender, RoutedEventArgs e)
    {
        SetTitleBar(true);
    }

    public void SetTitleBar(bool flage)
    {
        if(flage)
        {
            App.MainWindow.SetTitleBar(TitleBar);
        }
        else
        {
            App.MainWindow.SetTitleBar(null);
        }
    }

    public object Title
    {
        get
        {
            return (object)GetValue(TitleProperty);
        }
        set
        {
            SetValue(TitleProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(object), typeof(RootFrameTitleBar), new PropertyMetadata(default(object)));




    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var navigation = App.GetService<BiliStart.Contracts.Services.IAppNavigationService>();
        App.MainWindow.DispatcherQueue.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Low, () =>
        {
            if ((bool)navigation.CanRootFrameBack!)
            {
                navigation.GoBack(Contracts.Services.AppNavigationViewsEnum.RootFrame);
            }
        });
        
    }
}
