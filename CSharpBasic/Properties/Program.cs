namespace Properties
{
    // 구조체와 클래스 선정 기준
    // 값의 읽고 쓰기가 빈번하게 일어나면서 멤버 총합의 크기가 16바이트 이하일때 주로 구조체를 씀.
    // 확장의 가능성이 열려있어야 한다면 그래도 클래스를 써야함
    public class Human
    {
        public int age
        {
            get
            {
                return _age;
            }

            private set
            {
                _age = value;
            }
        }


        private int _age;

        public int GetAge()
        {
            return _age;
        }

        private void SetAge(int value)
        {
            _age = value;
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            Dummy dummy1 = new Dummy();
            dummy1.SetB(5);
            Dummy dummy2 = new Dummy();
            Console.WriteLine(dummy2.b);

            Console.WriteLine("Something");
        }

        
        static void PrintSomething()
        {
            Console.WriteLine("Something");
        }
    }
}