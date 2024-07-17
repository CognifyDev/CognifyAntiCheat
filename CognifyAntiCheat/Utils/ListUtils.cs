using System;
using System.Linq;
using Il2CppGenericCollections = Il2CppSystem.Collections.Generic;

namespace CognifyAntiCheat.Utils;

public static class ListUtils
{
    /// <summary>
    ///     打乱一个List
    /// </summary>
    /// <param name="list">欲打乱List</param>
    /// <typeparam name="T">元素类型</typeparam>
    /// <returns>打乱后的List</returns>
    public static System.Collections.Generic.List<T> Disarrange<T>(this System.Collections.Generic.IEnumerable<T> list)
    {
        return list.OrderBy(_ => new Random().Next()).ToList();
    }

    public static bool IsEmpty<T>(this System.Collections.Generic.List<T> list)
    {
        return list is not { Count: > 0 };
    }

    public static T GetOneAndDelete<T>(this System.Collections.Generic.List<T> list)
    {
        var obj = list[0];
        list.RemoveAt(0);
        return obj;
    }

    public static void ForEach<T>(this System.Collections.Generic.IEnumerable<T> collection, Action<T> action)
    {
        collection.ToList().ForEach(action);
    }

    // Using methods that contain arguments with keyword out such as TryGetValue(TKey key, out TValue value) may cause some unexcepted situations because of Il2CppInterop
    public static bool TryGetValueSafeIl2Cpp<TKey, TValue>(this Il2CppGenericCollections.Dictionary<TKey, TValue> dic,
        TKey key, out TValue safeValue) where TKey : notnull
    {
        if (dic.ContainsKey(key))
        {
            safeValue = dic[key];
            return true;
        }

        safeValue = default!;
        return false;
    }
}