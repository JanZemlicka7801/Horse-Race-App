namespace Horse_Race_App.objects
{
    public class Race
    {
        private string _name;
        private DateTime _startTime;
        private List<Horse> _horses;
        private int _allowedHorses;
        private bool _availability;

        public Race(string horseName, DateTime startTime, int allowedHorses)
        {
            _name = horseName;
            _startTime = startTime;
            _allowedHorses = allowedHorses;
            _horses = new List<Horse>();
            UpdateAvailability();
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Race name cannot be empty.");
                }
                _name = value;
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException($"Start time {value} is not valid.");
                }
                _startTime = value;
            }
        }

        public int AllowedHorses
        {
            get => _allowedHorses;
            set
            {
                if (_allowedHorses < 3 && _allowedHorses > 15)
                {
                    throw new ArgumentException("The number of allowed horses needs to be between 3 and 15 inclusive.");
                }
                _allowedHorses = value;
            }
        }

        public List<Horse> Horses => _horses;

        public bool Availability => _availability;

        private void UpdateAvailability()
        {
            _availability = _horses.Count < _allowedHorses;
        }

        public bool AddHorse(Horse horse)
        {
            if (IsRaceFull())
            {
                throw new ArgumentException("The race is already full.");
            }

            if (_horses.Contains(horse))
            {
                throw new ArgumentException("The horse is already in the race.");
            }

            UpdateAvailability();
            return AddHorse(horse);
        }

        public bool RemoveHorse(Horse horse)
        {
            if (!_horses.Any())
            {
                throw new ArgumentException("The race is empty");
            }

            if (!_horses.Contains(horse))
            {
                throw new ArgumentException("The horse is not in the race.");
            }

            UpdateAvailability();
            return RemoveHorse(horse);
        }

        public bool IsRaceFull()
        {
            return _horses.Count >= _allowedHorses;
        }

        public bool IsRaceEmpty()
        {
            return _horses.Count == 0;
        }

        public override string ToString()
        {
            string raceDetails = $"Race: {_name}, Start Time: {StartTime.ToShortTimeString()}, Allowed Horses: {_allowedHorses}, Current Horses: {_horses.Count}\nHorses:";
            foreach (var horse in _horses)
            {
                raceDetails += $"\n  {horse}";
            }
            return raceDetails;
        }
    }
}
