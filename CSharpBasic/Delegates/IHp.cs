using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class C
{
    A a;

    public void DoSomething()
    {
        a.onSomething = null;
    }
}

public class A
{
    int a = 1;
    float b = 1.0f;
    public class B
    {

    }

    public delegate bool OnSomethingHandler(int a, float b); // 대리자 형태 선언
    public event OnSomethingHandler onSomething; // 대리자 변수 선언
                                                 // event 한정자가 붙은 대리자는 외부클래스에서 대입연산이 제한된다. ( +=, -= 연산만 사용가능 )

    public event Action<int, float> action1;
    public Func<int, float, bool> func1;
    public Predicate<int> match1;

    public void Initialization()
    {
        func1 += IsSmallerThan;

        onSomething = IsBiggerThan;
        onSomething += IsSmallerThan;
        onSomething += IsSmallerThan;
        onSomething += IsSmallerThan;
        onSomething -= IsSmallerThan;

        
        onSomething(a, b); // 대리자 직접호출
        onSomething.Invoke(a, b); // 대리자 간접호출
        onSomething?.Invoke(a, b);

    }


    bool IsBiggerThan(int k, float m)
    {
        return k > m;
    }
    bool IsSmallerThan(int k, float m)
    {
        return k < m;
    }
    int Sum(int k, int m)
    {
        return k + m;
    }

}



namespace Delegates
{
    internal interface IHp
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
}
