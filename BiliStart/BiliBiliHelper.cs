using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart
{
    public static class BiliBiliHelper
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> values)
        {
            try
            {

                ObservableCollection<T> values1 = new ObservableCollection<T>();
                foreach (var item in values)
                {
                    values1.Add(item);
                }

                return values1;
            }
            catch (Exception)
            {
                return new ObservableCollection<T>();
            }
        }
    }
}
