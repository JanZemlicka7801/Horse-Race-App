using Horse_Race_App.src.objects;
using System;

namespace Horse_Race_App.src.people
{
    internal class HorseOwner
    {
        public void EnterHorseInRace(Race race, string name, DateTime date, string horseID)
        {
            Horse horse = new Horse(name, date, horseID);
            race.AddHorse(horse);
            Console.WriteLine($"{horse.HorseName} has been entered in {race.Name}.");
        }
    }
}
