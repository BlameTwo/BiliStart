using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace BiliStart.UI.Controls;
public sealed partial class BorderExpander : UserControl
{
    public BorderExpander()
    {
        this.InitializeComponent();
    }




    public object Icon
    {
        get
        {
            return (object)GetValue(IconProperty);
        }
        set
        {
            SetValue(IconProperty, value);
        }
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(object), typeof(BorderExpander), new PropertyMetadata(null));



    public object SubTitle
    {
        get
        {
            return (object)GetValue(SubTitleProperty);
        }
        set
        {
            SetValue(SubTitleProperty, value);
        }
    }

    // Using a DependencyProperty as the backing store for SubTitle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SubTitleProperty =
        DependencyProperty.Register("SubTitle", typeof(object), typeof(BorderExpander), new PropertyMetadata(null));



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

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(BorderExpander), new PropertyMetadata(null));




    public object Description
    {
        get
        {
            return (object)GetValue(DescriptionProperty);
        }
        set
        {
            SetValue(DescriptionProperty, value);
        }
    }

    public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register("Description", typeof(object), typeof(BorderExpander), new PropertyMetadata(null));



}
