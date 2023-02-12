using System;
using Profile;
using System.Collections.Generic;

internal interface IRepository : IDisposable
{

}

internal abstract class BaseRepository<TKey, TValue, TConfig> : IRepository
{
    private readonly Dictionary<TKey, TValue> _items;
    public IReadOnlyDictionary<TKey, TValue> Items => _items;

    public ProfilePlayer ProfilePlayer => _profilePlayer;

    private readonly ProfilePlayer _profilePlayer;

    protected BaseRepository(IEnumerable<TConfig> configs, ProfilePlayer profilePlayer = null)
    {
        _profilePlayer = profilePlayer;
        _items = CreteItems(configs);
    }


    public void Dispose() =>
        _items.Clear();


    private Dictionary<TKey, TValue> CreteItems(IEnumerable<TConfig> configs)
    {
        var items = new Dictionary<TKey, TValue>();

        foreach (TConfig config in configs)
            items[GetKey(config)] = CreateItem(config);

        return items;
    }

    protected abstract TKey GetKey(TConfig config);
    protected abstract TValue CreateItem(TConfig config);
}