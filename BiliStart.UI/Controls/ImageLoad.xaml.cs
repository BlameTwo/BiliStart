


using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;


namespace BiliStart.UI.Controls;
public sealed partial class ImageLoad : UserControl
{
    public ImageLoad()
    {
        this.InitializeComponent();
    }


    private bool isRing = false;


    public object Source
    {
        get => (object)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(object), typeof(ImageLoad), new PropertyMetadata(default(object), 
            new PropertyChangedCallback((s,e)=>OnChanged(s,e))));




    public Stretch Stretch
    {
        get =>(Stretch) GetValue(StretchProperty);
    set=> SetValue(StretchProperty, value);
    }

    public static readonly DependencyProperty StretchProperty =
        DependencyProperty.Register("Stretch", typeof(Stretch), typeof(Stretch), new PropertyMetadata(Stretch.UniformToFill));



    private static void OnChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != e.OldValue)
        {
            var instance = s as ImageLoad;
            if (instance.isRing)
                VisualStateManager.GoToState(instance, "RingLoading", false);
            else
                VisualStateManager.GoToState(instance, "Loading", false);
            if (e.NewValue == null)
            {
                instance.image.Source = null;
                VisualStateManager.GoToState(instance, "Failed", false);
            }
            else if (e.NewValue is string url)
            {
                var img = new BitmapImage() { DecodePixelType = DecodePixelType.Logical };
               
                instance.image.Source = img;
                if (string.IsNullOrEmpty(url))
                    instance.image.Source = null;
                else
                    img.UriSource = new Uri(url);
            }
            else if (e.NewValue is ImageSource image)
            {
                instance.image.Source = image;
            }
        }
    }

    private void Backimage_ImageOpened(object sender, RoutedEventArgs e)
    {
        VisualStateManager.GoToState(this, "Loaded", true);
    }

    private void Backimage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
    {
        VisualStateManager.GoToState(this, "Failed", true);
    }




    public BiliImageDisplayType DisplayType
    {
        get => (BiliImageDisplayType)GetValue(DisplayTypeProperty);
        set => SetValue(DisplayTypeProperty, value);
    }

    public static readonly DependencyProperty DisplayTypeProperty =
        DependencyProperty.Register("DisplayType", typeof(BiliImageDisplayType), typeof(ImageLoad), new PropertyMetadata(BiliImageDisplayType.None,new PropertyChangedCallback(TypeChanged)));

    private static void TypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != e.OldValue && e.NewValue is BiliImageDisplayType type)
        {
            var instance = d as ImageLoad;
            instance.isRing = false;
            if (type == BiliImageDisplayType.Rect)
            {
               
            }
            else if (type == BiliImageDisplayType.Wide)
            {
            }
            else if (type == BiliImageDisplayType.Ring)
            {
                instance.isRing = true;
            }
        }
    }

    public enum BiliImageDisplayType
    {
        None,
        Wide,
        Rect,
        Ring
    }
}
