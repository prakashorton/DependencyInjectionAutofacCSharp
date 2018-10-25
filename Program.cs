using Autofac;
using System;
using System.Collections.Generic;

namespace DIAutofac
{
    public class Car
    {
        private Engine engine;
        private ILog log;

        public Car(Engine engine, ILog log)
        {
            this.engine = engine;
            this.log = log;
        }

        public Car(Engine engine)
        {
            this.engine = engine;
            this.log = new EmailLog();
        }

        public void Go()
        {
            this.log.create("cAR");
            this.engine.AHead(100);
            Console.WriteLine("Car is running ");
        }
    }

    public class Engine
    {
        public Engine() { }

        public void AHead(int power)
        {
            Console.WriteLine("Engine is Ready " + power);
        }
    }

    public interface ILog
    {
        void create(string message);
    }

    public class ConsoleLog : ILog
    {
        void ILog.create(string message)
        {
            Console.WriteLine("hello console log " + message);
        }
    }

    public class EmailLog : ILog
    {
        void ILog.create(string message)
        {
            Console.WriteLine("hello EmailLog log " + message);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            // Class based
            // builder.RegisterType<ConsoleLog>()
            //    .As<ILog>().AsSelf();

            var log = new ConsoleLog();
            // Instance based
            builder.RegisterInstance(log).As<ILog>();

            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();
            //builder.RegisterType<Car>().UsingConstructor(typeof(Engine));

            // builder.RegisterGeneric(typeof(List<>)).As<IList>();
            IContainer container = builder.Build();
            Car afCar = container.Resolve<Car>();
            afCar.Go();

            //Engine engine = new Engine();
            //Car car = new Car(engine);
            //car.Go();

            Console.ReadLine();
        }
    }
}
