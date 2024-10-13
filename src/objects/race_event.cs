using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse_Race_App.src.objects
{
    internal class Race_Event
    {
        private string eventName;
        private string location;
        public List<Race> races { get; set; }

        public string EventName {
            get => eventName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Event name cannot be empty or null.");
                }
                eventName = value;
            }
        }

        public string Location
        {
            get => location;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Location name cannot be empty or null.");
                }
                location = value;
            }
        }

        public void AddRace(Race race)
        {
            races.Add(race);
        }

        public void RemoveRace(Race race)
        {
            races.Remove(race);
        }

        public override string ToString()
        {
            string eventDetails = $"Event: {EventName}\nLocation: {Location}\nRaces:";
            foreach (var race in races)
            {
                eventDetails += $"\n{race}\n";
            }
            return eventDetails;
        }
    }
}
