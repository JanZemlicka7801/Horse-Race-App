using Horse_Race_App.src.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse_Race_App.src.people
{
    internal class HorseOwner
    {
        public void EnterHorseInRace(Race race, string name, DateTime date, string horse)
        {
            race.AddHorse(new Horse(name, date, horse));
            Console.WriteLine($"{name} has been entered in {race.Name}.");
        }
    }
}
