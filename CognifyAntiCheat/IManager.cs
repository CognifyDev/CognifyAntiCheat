using CognifyAntiCheat.Exception;

namespace CognifyAntiCheat;

public interface IManager<T>
{
    public string GetName();
    
    public void Register(T t)
    {
        throw new NotAvailableException($"Register for {GetName()} is not available!");
    }

    public void Remove(T t)
    {
        throw new NotAvailableException($"Remove for {GetName()} is not available!");   
    }

    public void Clear()
    {
        throw new NotAvailableException($"Clear for {GetName()} is not available!");
    }

    public void ReloadManager()
    {
        throw new NotAvailableException($"ReloadManager for {GetName()} is not available!");
    }

    public T[] Get()
    {
        throw new NotAvailableException($"Get for {GetName()} is not available!");
    }
}