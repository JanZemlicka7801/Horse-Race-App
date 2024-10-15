using Horse_Race_App.objects;

namespace Horse_Race_App.people
{
    internal class HorseOwner
    {
        public void EnterHorseInRace(Race race, string name, DateTime date, string horseId)
        {
            Horse horse = new Horse(name, date, horseId);
            race.AddHorse(horse);
            Console.WriteLine($"{horse.HorseName} has been entered in {race.Name}.");
        }
    }
}
