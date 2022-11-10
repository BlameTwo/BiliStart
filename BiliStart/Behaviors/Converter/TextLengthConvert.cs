using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace BiliStart.Behaviors.Converter
{
    internal class TextLengthConvert : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {


            string str = value.ToString()!;
            int value2 = int.Parse(parameter.ToString()!);
            if (str.Length > value2)
            {
                return str.Substring(0, value2) + "...";
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }


    internal class TickConvert : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
