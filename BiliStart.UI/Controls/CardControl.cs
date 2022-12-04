using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace BiliStart.UI.Controls;
public class CardControl:Microsoft.UI.Xaml.Controls.ContentControl
{


    public Symbol Icon
    {
        get
        {
            return (Symbol)GetValue(IconProperty);
        }
        set
        {
            SetValue(IconProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(Symbol), typeof(CardControl), new PropertyMetadata(null));






    public Brush IconForeground
    {
        get
        {
            return (Brush)GetValue(IconForegroundProperty);
        }
        set
        {
            SetValue(IconForegroundProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for IconForeground.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IconForegroundProperty =
        DependencyProperty.Register("IconForeground", typeof(Brush), typeof(CardControl), new PropertyMetadata(null));





    public string Header
    {
        get
        {
            return (string)GetValue(HeaderProperty);
        }
        set
        {
            SetValue(HeaderProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(CardControl), new PropertyMetadata(""));



}
