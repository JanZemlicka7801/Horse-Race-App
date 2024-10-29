namespace Horse_Race_App.objects
{
    public class RaceEvents
    {
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public List<Race> Races { get; set; }
        public int NumberOfRaces { get; set; }

        public RaceEvents(string eventName, string location, DateTime startDate, List<Race> race, int numberOfRaces)
        {
            if (!ValidateEventName(eventName) || !ValidateEventLocation(location) || !ValidateNumberOfRaces(numberOfRaces) || !ValidateStartDate(startDate))
            {
                Console.WriteLine("Invalid element was entered.");
                return;
            }
            NumberOfRaces = numberOfRaces;
            EventName = eventName;
            Location = location;
            Races = race;
        }

        public bool ValidateStartDate(DateTime start)
        {
            if (StartDate <= DateTime.Today)
            {
                Console.WriteLine("Invalid date, cannot be in the past or today.");
                return false;
            }

            return true;
        }
        
        //validates number of races
        public bool ValidateNumberOfRaces(int number)
        {
            if (number < 1 || number > 30)
            {
                Console.WriteLine("Invalid number was entered. The possible number can be between 1 and 30.");
                return false;
            }
            return true;
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
        
        //function for checking if the event is full
        private bool IsFullEvent()
        {
            return Races.Count >= NumberOfRaces;
        }
        
        //checking if the event doesn't have races
        public bool IsEmptyEvent()
        {
            return Races.Count == 0;
        }
        
        //adds a race to an event
        public bool AddRace(Race race)
        {
            if (Races.Contains(race) || IsFullEvent())
            {
                Console.WriteLine("Error has occured when adding a race...");
                return false;
            }
            return true;
        }
        
        //removes race from the list of races
        public bool RemoveRace(Race race)
        {
            if (!Races.Contains(race) || IsEmptyEvent())
            {
                Console.WriteLine("There is an error in removing a race...");
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
