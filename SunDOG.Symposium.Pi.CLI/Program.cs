using SunDOG.Symposium.Pi.Core;

namespace SunDOG.Symposium.Pi.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            INA219Service sensor1 = new INA219Service(1);
            INA219Service sensor2 = new INA219Service(7);

            Console.WriteLine("Sensors Initialized");

            Thread.Sleep(100);

            Console.WriteLine("Testing");

            sensor1.Test();
            sensor2.Test();
            for (int i = 0; i < 100; i ++)
            {
                // Print with :F3 formatting
                Console.WriteLine($"Sensor 1 Current : {sensor1.ReadCurrentMilli():F3} mA");
                Console.WriteLine($"Sensor 2 Current : {sensor2.ReadCurrentMilli():F3} mA");

                Thread.Sleep(500);
            }
           
        }
    }
}
