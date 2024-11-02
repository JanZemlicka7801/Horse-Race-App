using Horse_Race_App.objects;
using Horse_Race_App.utils;

namespace Horse_Race_App.people
{
    public class HorseOwner
    {
        public static void EnterHorseInRace(Race race, string name, DateTime date, string horseId)
        {
            if (!Horse.ValidateHorseId(horseId, FileUtils.DataToHorses()) || 
                !Horse.ValidateHorseAge(date) || 
                !Horse.ValidateHorseName(name))
            {
                Console.WriteLine("Invalid horse properties!");
                return;
            }

            Horse horse = new Horse(name, date, horseId);

            Console.WriteLine(race.AddHorse(horse)
                ? $"{horse.HorseName} has been entered in {race.Name}."
                : "Horse could not be entered in the race.");
        }

        public void RemoveHorseFromRace(Race race, string name)
        {
            Horse horseToRemove = race.Horses.FirstOrDefault(h => h.HorseName == name);

            if (horseToRemove != null)
            {
                if (race.RemoveHorse(horseToRemove))
                {
                    Console.WriteLine($"Horse {name} has been removed from {race.Name}.");
                }
                else
                {
                    Console.WriteLine($"Horse {name} could not be removed from {race.Name}.");
                }
            }
            else
            {
                Console.WriteLine($"Horse {name} is not in the {race.Name} race.");
            }
        }
    }
}