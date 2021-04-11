using System;

namespace TestKaastrupAndersen
{
    interface IFacade
    {
        string GenerateSalaryMessage(DateTime startedWork);
    }

    class Program
    {
        static void Main(string[] args)
        {
            IFacade facade = new Facade();

            while (true)
            {
                Console.WriteLine("Hello Dear Developer!");
                Console.WriteLine($"Please tell when you started working Format 'Month Day Hour Minutes' fx {DateTime.Now.Month} {DateTime.Now.Day} {DateTime.Now.Hour} {DateTime.Now.Minute} ");
                var input = Console.ReadLine();

                var parts = input.Split(' ');
                try
                {
                    var startedWorking = new DateTime(DateTime.Now.Year, int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), 0);
                    string messageToDeveloper = facade.GenerateSalaryMessage(startedWorking);

                    Console.WriteLine(messageToDeveloper);
                }
                catch (Exception)
                {
                    Console.WriteLine($"{input} is not an accepted format");
                }
            }

        }
    }
}
