using Horse_Race_App.objects;

namespace Horse_Race_App.people
{
    internal class RacecourseManager
    {
        public List<RaceEvents> Events {  get; set; }

        //main list that holds all race events
        public RacecourseManager()
        {
            Events = new List<RaceEvents>();
        }

        //function for creating a race event and log it inside the manager
        public RaceEvents CreateRaceEvent(string eventName, string location, int numberOfRaces)
        {
            foreach (var raceEvent in Events)
            {
                if (raceEvent.EventName.Equals(eventName))
                {
                    Console.WriteLine($"Event {eventName} is already in the system.");
                }
            }

            var RaceEvent = new RaceEvents(eventName, location, numberOfRaces);
            Events.Add(RaceEvent);
            return RaceEvent;
        }

        //function for deleting the race event from the manager
        public bool DeleteRaceEvent(string eventName)
        {
            foreach (var raceEvent in Events)
            {
                if (raceEvent.EventName.Equals(eventName))
                {
                    Events.Remove(raceEvent);
                    return true;
                }
            }
            Console.WriteLine($"Event {eventName} is not in the system. In this case, the race event won't be deleted.");
            return false;
        }
    }
}
