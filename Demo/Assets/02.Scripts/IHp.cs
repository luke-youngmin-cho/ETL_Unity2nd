using System;
using UnityEngine;

public interface IHp 
{
    float hpValue { get; }
    float hpMax { get; }
    float hpMin { get; }


    event Action onHpMax;
    event Action onHpMin;
    event Action<float> onHpChanged;
    event Action<float> onHpRecovered;
    event Action<float> onHpDepleted;

    void RecoverHp(object subject, float amount);
    void DepleteHp(object subject, float amount);
}
