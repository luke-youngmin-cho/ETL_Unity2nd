using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldDataModel : IDataModel
{
    public int gold
    {
        get => _gold;
        set
        {
            if (_gold < 0)
                return;

            _gold = value;
            onGoldChanged?.Invoke(value);
        }
    }
    private int _gold;
    public event Action<int> onGoldChanged;

    public GoldDataModel()
    {
        onGoldChanged += value => Save();
    }

    public void Save()
    {
        // todo -> Save
    }

    public void Load()
    {
        gold = 0; // todo -> load
    }
}
