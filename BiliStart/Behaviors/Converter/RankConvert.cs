using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace BiliStart.Behaviors.Converter
{
    internal class TimeConvert : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {

            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(long.Parse(value.ToString()) + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt.ToString("G");
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }

    internal class CountForNumber : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (int.Parse(value.ToString()!) > 10000)
            {
                return (int.Parse(value.ToString()!) / 10000) + "w";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
