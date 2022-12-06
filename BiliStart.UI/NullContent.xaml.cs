


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




namespace BiliStart.UI;
public sealed partial class NullContent : UserControl
{
    public NullContent()
    {
        this.InitializeComponent();
    }



    public Visibility _NummPopup
    {
        get=>
            (Visibility)GetValue(_NummPopupProperty);
        set =>
            SetValue(_NummPopupProperty, value);
    }

    // Using a DependencyProperty as the backing store for _NummPopup.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty _NummPopupProperty =
        DependencyProperty.Register("_NummPopup", typeof(Visibility), typeof(NullContent), new PropertyMetadata(Visibility.Collapsed));





    public string _TipMessage
    {
        get=> (string)GetValue(_TipMessageProperty);
        set => SetValue(_TipMessageProperty, value);
    }

    // Using a DependencyProperty as the backing store for _TipMessage.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty _TipMessageProperty =
        DependencyProperty.Register("_TipMessage", typeof(string), typeof(NullContent), new PropertyMetadata("不要在意"));


}
