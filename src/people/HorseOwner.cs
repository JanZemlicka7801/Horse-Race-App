using Horse_Race_App.objects;

namespace Horse_Race_App.people
{
    internal class HorseOwner
    {
        public void EnterHorseInRace(Race race, string name, DateTime date, string horseId)
        {
            Horse horse = new Horse(name, date, horseId);
            Console.WriteLine(race.AddHorse(horse)
                ? $"{horse.HorseName} has been entered in {race.Name}."
                : "Horse has not been entered.");
        }

        public void RemoveHorseFromRace(Race race, string name)
        {
            Horse horseToRemove = race.Horses.Where(h => h.HorseName == name).FirstOrDefault();

            if (horseToRemove != null)
            {
                if (race.RemoveHorse(horseToRemove))
                {
                    Console.WriteLine($"Horse {name} has been removed from {race.Name}.");
                    race.RemoveHorse(horseToRemove);
                }
                else
                {
                    Console.WriteLine($"Horse {name} has not been removed from {race.Name}.");
                }
            }
            else
            {
                Console.WriteLine($"Horse {name} is not in the {race.Name} race.");
            }
        }
    }
}
