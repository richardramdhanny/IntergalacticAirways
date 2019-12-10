using System;

namespace Starships
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Intergalactic Starship Booking System");
            Console.WriteLine("Please enter the number of passengers for your journey:");
            
            var numberOfPassengers = Console.ReadLine();
            int.TryParse(numberOfPassengers, out int validatedNumberOfPassengers);

            if (validatedNumberOfPassengers > 0)
            {
                var data = new Data();
                var result = data.FindStarships(validatedNumberOfPassengers);

                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
            } 
            else
            {
                Console.WriteLine($"You selected an invalid number of passengers");
            }
        }
    }
}
