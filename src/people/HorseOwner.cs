using Horse_Race_App.src.objects;
using System;

namespace Horse_Race_App.src.people
{
    internal class HorseOwner
    {
        public void EnterHorseInRace(Race race, string name, DateTime date, string horseID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Horse name cannot be empty.");
                }

                race.AddHorse(new Horse(name, date, horseID));
                Console.WriteLine($"{name} has been entered in {race.Name}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to enter horse into race: {ex.Message}");
            }
        }
    }
}
