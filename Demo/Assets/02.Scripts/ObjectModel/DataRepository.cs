using System;
using System.Collections.Generic;
using TMPro;

public class DataRepository : SingletonBase<DataRepository>
{
    private Dictionary<Type, IDataModel> _datum = new Dictionary<Type, IDataModel>();

    public bool TryGet<T>(out IDataModel data)
        where T : IDataModel
    {
        if (_datum.TryGetValue(typeof(T), out data))
            return true;

        data = null;
        return false;
    }

    public void Save<T>()
    {
        // todo -> save typeof(T) data.
    }

    public void Load<T>()
    {

    }

    public override void Initialize()
    {
        base.Initialize();
        _datum.Add(typeof(GoldDataModel), new GoldDataModel());
        _datum.Add(typeof(InventoryDataModel), new InventoryDataModel());
        _datum.Add(typeof(ItemShopDataModel), new ItemShopDataModel());
    }
}