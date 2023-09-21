using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class InteractableBox : MonoBehaviour, IHp, IPointerClickHandler
{
    public float hpValue
    {
        get => _hpValue;
        private set
        {
            if (_hpValue == value)
                return;
                        
            value = Mathf.Clamp(value, hpMin, hpMax);
            _hpValue = value;
            onHpChanged?.Invoke(value);
            if (value == hpMin)
                onHpMin?.Invoke();
            else if (value == hpMax)
                onHpMax?.Invoke();
        }
    }

    public float hpMax
    {
        get
        {
            return _hpMax;
        }
    }

    public float hpMin => _hpMin;

    public event Action onHpMax;
    public event Action onHpMin;
    public event Action<float> onHpChanged;
    public event Action<float> onHpRecovered;
    public event Action<float> onHpDepleted;

    [SerializeField] private float _hpValue;
    [SerializeField] private float _hpMax;
    [SerializeField] private float _hpMin;

    public void DepleteHp(object subject, float amount)
    {
        hpValue -= amount;
        onHpDepleted?.Invoke(amount);
    }

    public void RecoverHp(object subject, float amount)
    {
        hpValue += amount;
        onHpRecovered?.Invoke(amount);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DepleteHp(this, 10.0f);
    }

    private void Awake()
    {
        onHpChanged += (value) => Debug.Log($"Current hp : {value}");
        onHpChanged = null;
        //onHpChanged += PrintLogCurrentHPValue;

        Random.Range(0.0f, 1.0f);
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        using (IEnumerator<int> e1 = list1.GetEnumerator())
        using (IEnumerator<int> e2 = list2.GetEnumerator())
        {
            while (e1.MoveNext() && e2.MoveNext())
            {
                bool result = e1.MoveNext() == e2.MoveNext();
            }
            e1.Reset();
            e2.Reset();
        }

    }

    // 람다식 : 익명함수 (이름없는 함수)
    // 람다식을 작성하는 원리 : 컴파일러가 알아서 판단할 수 있는 내용들을 생략하고 () 옆에 => 기호를 써서 람다식이라고 명시함
    //(value) => Debug.Log($"Current hp : {value}")

    //private void PrintLogCurrentHPValue(float value)
    //{
    //    Debug.Log($"Current hp : {value}");
    //}
}
