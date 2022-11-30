using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace BiliStart.Behaviors.Converter;
public class PlayerSliderConvert : IValueConverter
{
    public static PlayerSliderConvert SliderTimeConvert = new PlayerSliderConvert();
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var date = TimeSpan.FromMilliseconds((double)value);

        return date.ToString(@"hh\:mm\:ss");
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
