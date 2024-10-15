namespace Horse_Race_App.objects
{
    public class RaceEvents
    {
        private string _eventName;
        private string _location;
        public List<Race> Races { get; set; }

        public RaceEvents(string eventName, string location)
        {
            EventName = eventName;
            Location = location;
            Races = new List<Race>();
        }

        public string EventName {
            get => _eventName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Event name cannot be empty or null.");
                }
                _eventName = value;
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Location name cannot be empty or null.");
                }
                _location = value;
            }
        }

        public void AddRace(Race race)
        {
            Races.Add(race);
        }

        public void RemoveRace(Race race)
        {
            Races.Remove(race);
        }

        public override string ToString()
        {
            string eventDetails = $"Event: {EventName}\nLocation: {Location}\nRaces:";
            foreach (var race in Races)
            {
                eventDetails += $"\n{race}\n";
            }
            return eventDetails;
        }
    }
}
