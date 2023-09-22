using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 코루틴 
// 어떤 루틴 실행 이후 다른 루틴이 실행되도록하는 구조

// 유니티엔진의 코루틴
// Monobehaviour 의 Update 이후 실행되는 구조 -> Monobehaviour 비활성화시 코루틴 이어서 실행안됨
// IEnumerator 로 코루틴이 구현되어있음. -> Update 이후 해당 Enumerator의 MoveNext() 가 호출되는 구조
public class Test_Coroutine : MonoBehaviour
{
    bool _isCorouting;
    Coroutine _coroutine;
    IEnumerator _enumerator; // 가비지컬렉션 최소화
    WaitForSeconds _waitForSeconds;
    float _delay;
    private void Awake()
    {
        _enumerator = C_Refresh();
        _waitForSeconds = new WaitForSeconds(3.5f);
    }

    private void Update()
    {
        if (_isCorouting)
        {
            StopCoroutine(_coroutine);
        }

        // Enumerator 객체를 코루틴으로서 등록한 프레임에는 MoveNext() 가 호출이 안됨. 
        // -> 코루틴 시작 전 초기화할 내용이 있으면 Enumerator 의 생성자에서초기화내용을 쓰던지,
        // IEnumerator/IEnumerable 반환 함수에서 yield 로 구현했다면 StartCoroutine 으로 등록하기 전에 초기화내용을 호출해야함
        _isCorouting = true;
        _coroutine = StartCoroutine(C_Refresh());


        if (_enumerator != null)
        {
            StopCoroutine(_enumerator);
            _enumerator = null;
        }

        _enumerator = C_Refresh();
        StartCoroutine(_enumerator);
    }

    // yield 키워드 
    // Enumerator 객체를 간소화한 표현.
    // IEnumerator / IEnumerable 을 반환하는 함수내에서 사용하면, 
    // 컴파일 타임에 해당 Enumerator 객체를 정의한다.
    IEnumerator C_Refresh()
    {
        yield return null;
        yield return _waitForSeconds;
        yield return new WaitForSeconds(_delay);
        yield return null;
        _coroutine = null;
        _isCorouting = false;
        //return new C_RefreshEnum();
    }
    
    /*struct C_RefreshEnum : IEnumerator
    {
        public object Current => throw new System.NotImplementedException();
        private int _index;
        private List<object> _objects = new List<object>() { null, new WaitForSeconds(3.5f), null};
        private float _timeMark;

        public bool MoveNext()
        {
            switch (_index)
            {
                case 0:
                    {
                        _index++;
                        _timeMark = Time.time;
                    }
                    break;
                case 1:
                    {
                        if (Time.time - _timeMark >= 3.5f)
                        {
                            _index++;
                        }
                    }
                    break;
                case 2:
                    {
                        _index++;
                    }
                    break;
            }
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }*/

}
