using Horse_Race_App.src.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse_Race_App.src.people
{
    internal class RacecourseManager
    {
        public List<RaceEvent> Events {  get; set; }

        public RacecourseManager()
        {
            Events = new List<RaceEvent>();
        }

        public RaceEvent CreateRaceEvent(string eventName, string location)
        {
            var raceEvent = new RaceEvent(eventName, location);
            Events.Add(raceEvent);
            return raceEvent;
        }

        public void AddRaceToEvent(RaceEvent raceEvent, Race race)
        {
            raceEvent.AddRace(race);
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
