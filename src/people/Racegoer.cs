using Horse_Race_App.src.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse_Race_App.src.people
{
    internal class Racegoer
    {
        public void ViewUpcomingEvents(List<RaceEvent> events)
        {
            Console.WriteLine("Upcoming Events:");
            foreach (var raceEvent in events)
            {
                Console.WriteLine(raceEvent);
            }
        }
    }
}
