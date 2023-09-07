using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider _hp;
    [SerializeField] private InteractableBox _box;

    private void Awake()
    {
        _hp.minValue = _box.hpMin;
        _hp.maxValue = _box.hpMax;
        _hp.value = _box.hpValue;

        _box.onHpChanged += (value) => _hp.value = value;

        //_box.onHpChanged += Refresh;
    }

    void Refresh(float value)
    {
        _hp.value = value;
    }
}
