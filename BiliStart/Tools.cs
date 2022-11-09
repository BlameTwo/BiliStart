using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliStart;
public static class Tools
{
    public static ObservableCollection<T> ToObservableCollection<T>(this List<T> list)
    {
        ObservableCollection<T> collection = new ObservableCollection<T>(); 
        foreach (var item in list)
        {
            collection.Add(item);
        }
        return collection;
    }
}
