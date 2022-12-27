using System.Collections.ObjectModel;
using BiliBiliAPI.Models.PGC;

namespace BiliStart;
public static class Tools
{
    public static ObservableCollection<T> ToObservableCollection<T>(this List<T> list)
    {
        if (list == null) return null;
        ObservableCollection<T> collection = new ObservableCollection<T>();
        foreach (var item in list)
        {
            collection.Add(item);
        }
        return collection;
    }

    /// <summary>
    /// 反射类
    /// </summary>
    /// <typeparam name="T">子类</typeparam>
    /// <typeparam name="T1">父类</typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static T1 ClassConvert<T,T1>(this T data)
        where T :class,new()
        where T1:class,new()
    {
        T1 returnvalue = new T1();
        System.Reflection.PropertyInfo[] properties = data.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        foreach (var item in properties)
        {
            //循环遍历属性
            if (item.CanRead && item.CanWrite)
            {
                //进行属性拷贝
                item.SetValue(returnvalue, item.GetValue(data));
            }
        }
        return returnvalue;
    }
}
