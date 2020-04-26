using System;

namespace VerySimpleDI
{
    public interface IService1
    {
        void Display();
    }

    public interface IService2
    {
        void Show();
    }

    public class Service1 : IService1, IDisposable
    {
        public void Display()
        {
            Console.WriteLine("Service1 displays itself.");
        }

        public void Dispose()
        {
            Console.WriteLine("dispose Service1");
        }
    }

    public class Service2 : IService2, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("dispose Service2");
        }

        public void Show()
        {
            Console.WriteLine("Service2 shows itself.");
        }
    }

    public interface IService3
    {
        void DoSomething();
    }

    public class Service3 : IService3, IDisposable
    {
        private IService1 _service1;
        private IService2 _service2;

        public Service3(IService1 service1, IService2 service2)
        {
            _service1 = service1;
            _service2 = service2;
        }

        public void DoSomething()
        {
            Console.WriteLine("Service3 DoSomething starts");
            _service1.Display();
            _service2.Show();
            Console.WriteLine("Service3 DoSomething ends");

        }

        public void Dispose()
        {
            Console.WriteLine("dispose Service3");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var injector = new Injector();
            injector.AddTransient<IService1, Service1>();
            injector.AddTransient<IService2, Service2>();
            injector.AddTransient<IService3, Service3>();

            var service3 = injector.GetInstance<IService3>();
            service3.DoSomething();
        }
    }
}
