using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace BiliStart.UI.Controls;
public class ActionButton:Button
{
    protected override void OnApplyTemplate() => base.OnApplyTemplate();

    public bool IsChecked
    {
        get
        {
            return (bool)GetValue(IsCheckedProperty);
        }
        set
        {
            SetValue(IsCheckedProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register("IsChecked", typeof(bool), typeof(ActionButton), new PropertyMetadata(null));


}
