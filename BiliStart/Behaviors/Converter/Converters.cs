using BiliBiliAPI;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart.Behaviors.Converter
{
    public class CidConverters : IValueConverter
    {
        CidFormat Format = new CidFormat();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Cid cid)
            {
                return Format.FromEnum(cid);
            }
            return "~什么也没有";
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }


    public class AnimationItemRating : IValueConverter
    {
        public static readonly IValueConverter Instance = new AnimationItemRating();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                return value;
            }
            else
            {
                return "无评分";
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
