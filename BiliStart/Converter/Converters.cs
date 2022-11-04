using BilibiliAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BiliStart.Converter
{
    internal class CidConverters : IValueConverter
    {
        CidFormat Format = new CidFormat();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Cid cid)
            {
                return Format.FromEnum(cid);
            }
            return "~什么也没有";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class AnimationItemRating : IValueConverter
    {
        public static readonly IValueConverter Instance = new AnimationItemRating();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                return value;
            }
            else
            {
                return "无评分";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
