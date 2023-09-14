using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    internal class Dummy
    {
        public static int a;
        public int b;

        // 클래스 생성자
        // 힙영역에 인스턴스멤버변수들 전부 할당한후, BSS/DATA 영역에 있던 초기값데이터로 전부 초기화하고 생성한 객체에대한 참조를 반환
        public Dummy() { }

        // 소멸자
        // 힙영역에 할당된 객체를 메모리에서 해제하는 함수
        ~Dummy() { }

        // this 객체 자기자신 참조 키워드
        // 인스턴스멤버함수 정의시 매번 참조할 인스턴스를 파라미터로 받는 내용을 정의하는것을 생략할 수 있도록 도와주는 키워드
        public void SetB(int value)
        {
            b = value;
        }
    }
}
