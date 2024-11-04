namespace Horse_Race_App.objects
{
    public class RaceEvents
    {
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public List<Race> Races { get; set; }
        public int NumberOfRaces { get; set; }

        /**
         * Main constructor for race event with properties such as name, location, start date, list and number of races.
         */
        public RaceEvents(string eventName, string location, DateTime startDate, List<Race> race, int numberOfRaces)
        {
            var isValid = true;
            if (!ValidateEventName(eventName)) isValid = false;
            else if (!ValidateEventLocation(location)) isValid = false;
            else if (!ValidateNumberOfRaces(numberOfRaces)) isValid = false;
            else if (!ValidateStartDate(startDate)) isValid = false;
    
            EventName = eventName;
            Location = location;
            StartDate = startDate;
            Races = race;
            NumberOfRaces = numberOfRaces;

            if (!isValid)
            {
                Console.WriteLine("Invalid elements were entered for RaceEvents.");
            }
        }

        /*
         * Validates date of the event to be in the future.
         */
        private static bool ValidateStartDate(DateTime start)
        {
            if (start > DateTime.Today)
            {
                Console.WriteLine("Invalid date, cannot be in the past or today.");
                return true;
            }
            return false;
        }
        
        /*
         * Validates number of races to be from 1 to 30 per event.
         */
        private bool ValidateNumberOfRaces(int number)
        {
            if (number <= 0 || number > 30)
            {
                Console.WriteLine("Invalid number was entered. The possible number can be between 1 and 30.");
                return false;
            }
            return true;
        }
        
        /*
         * Validates the name if it is provided and does not equal null.
         */
        private bool ValidateEventName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name is required");
                return false;
            }

            return true;
        }
        
        /*
         * Validates the location if it is provided and does not equal null.
         */
        private bool ValidateEventLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                Console.WriteLine("Location is required");
                return false;
            }
            return true;
        }
        
        /*
         * Checks if the event's capacity is full.
         */
        private bool IsFullEvent()
        {
            return Races.Count == NumberOfRaces;
        }
        
        /*
         * Checks if the event is empty.
         */
        public bool IsEmptyEvent()
        {
            return Races.Count == 0;
        }
        
        /**
         * Adds a race inside the race event.
         */
        public bool AddRace(Race race)
        {
            if (Races.Contains(race) || IsFullEvent())
            {
                Console.WriteLine("Error has occured when adding a race...");
                return false;
            }
            return true;
        }
        
        /**
         * Removes a race from race event.
         */
        public bool RemoveRace(Race race)
        {
            if (!Races.Contains(race) || IsEmptyEvent())
            {
                Console.WriteLine("There is an error in removing a race...");
                return false;
            }
            return Races.Remove(race);
        }

        /**
         * Displays data in nice formatted text.
         */
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
