﻿using Horse_Race_App.objects;

namespace Horse_Race_App.people
{
    public class Racegoer
    {
        public void ViewUpcomingEvents(List<RaceEvents> events)
        {
            if (events.Count == 0)
            {
                Console.WriteLine("There are no upcoming events. Please try it again later.");
                return;
            }
            
            Console.WriteLine("Upcoming Events:");
            foreach (var raceEvent in events)
            {
                Console.WriteLine(raceEvent);
                foreach (var race in raceEvent.Races)
                {
                    Console.WriteLine($"  {race}");
                    foreach (var horse in race.Horses)
                    {
                        Console.WriteLine($"    {horse}");
                    }
                }
            }
        }
    }
}
