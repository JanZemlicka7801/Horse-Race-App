namespace Horse_Race_App.objects
{
    public class RaceEvents
    {
        private string EventName { get; set; }
        private string Location { get; set; }
        public List<Race> Races { get; set; }

        public RaceEvents(string eventName, string location)
        {
            if (!ValidateEventName(eventName) || !ValidateEventLocation(location))
            {
                Console.WriteLine("Invalid element was entered.");
                return;
            }
            EventName = eventName;
            Location = location;
            Races = new List<Race>();
        }
        
        //validates event name
        public bool ValidateEventName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name is required");
                return false;
            }

            return true;
        }
        
        //validates location
        public bool ValidateEventLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                Console.WriteLine("Location is required");
                return false;
            }
            return true;
        }
        
        //adds a race to an event
        public bool AddRace(Race race)
        {
            if (Races.Contains(race))
            {
                Console.WriteLine("Error has occured when adding a race...");
                return false;
            }
            return true;
        }
        
        //removes race from the list of races
        public bool RemoveRace(Race race)
        {
            if (!Races.Contains(race))
            {
                Console.WriteLine("Race not found in the event...");
                return false;
            }
            return Races.Remove(race);
        }

        //from link to a readable format
        public override string ToString()
        {
            string eventDetails = $"{nameof(EventName)}: {EventName}, {nameof(Location)}: {Location}, {nameof(Races)}:";
            if (Races.Count == 0)
            {
                eventDetails += "\n  No races available.";
            }
            else
            {
                foreach (var race in Races)
                {
                    eventDetails += $"\n  {race}\n";
                }
            }
            return eventDetails;
        }
    }
}
