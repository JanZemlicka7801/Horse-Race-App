using Horse_Race_App.objects;

namespace Horse_Race_App.people
{
    public class Racegoer
    {
        public static void ViewUpcomingEvents(List<RaceEvents> events)
        {
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
