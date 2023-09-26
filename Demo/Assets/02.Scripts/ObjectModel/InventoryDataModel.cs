using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public struct ItemPair
{
    public int id;
    public int num;
}

public class InventoryDataModel : ObservableCollection<ItemPair>, IDataModel
{
    public void Save()
    {

    }

    public void Load()
    {

    }
}
