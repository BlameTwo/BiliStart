using System.Collections.ObjectModel;

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
}
