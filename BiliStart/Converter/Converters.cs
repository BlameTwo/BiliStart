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
}
