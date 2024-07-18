using System;

namespace CognifyAntiCheat;

public interface IManager<T>
{
    public string GetName();
    
    public void Register(T t)
    {
        throw new NotImplementedException($"Register for {GetName()} is not available!");
    }

    public void Remove(T t)
    {
        throw new NotImplementedException($"Remove for {GetName()} is not available!");   
    }

    public void Clear()
    {
        throw new NotImplementedException($"Clear for {GetName()} is not available!");
    }

    public void ReloadManager()
    {
        throw new NotImplementedException($"ReloadManager for {GetName()} is not available!");
    }

    public T[] Get()
    {
        throw new NotImplementedException($"Get for {GetName()} is not available!");
    }
}