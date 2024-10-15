using Horse_Race_App.objects;

namespace Horse_Race_App.people
{
    internal class RacecourseManager
    {
        public List<RaceEvents> Events {  get; set; }

        public RacecourseManager()
        {
            Events = new List<RaceEvents>();
        }

        public RaceEvents CreateRaceEvent(string eventName, string location)
        {
            foreach (var RaceEvent in Events)
            {
                if (RaceEvent.EventName.Equals(eventName, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Event name already exists");
                }
            }
            
            var raceEvent = new RaceEvents(eventName, location);
            Events.Add(raceEvent);
            return raceEvent;
        }

        public void AddRaceToEvent(RaceEvents raceEvent, Race race)
        {
            raceEvent.AddRace(race);
        }

        public void RemoveRaceFromEvent(RaceEvents raceEvent, Race race)
        {
            raceEvent.RemoveRace(race);
        }

        public override string ToString()
        {
            string managerDetails = "Racecourse Manager Events:";
            foreach (var raceEvent in Events)
            {
                managerDetails += $"\n{raceEvent}";
            }
            return managerDetails;
        }
    }
}
