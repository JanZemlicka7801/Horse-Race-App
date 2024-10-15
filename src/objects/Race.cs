using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse_Race_App.src.objects
{
    public class Race
    {
        private string name { get; set; }
        private DateTime startTime;
        private List<Horse> horses;
        private int allowedHorses;
        private bool availability;

        public Race(string HorseName, DateTime StartTime, int AllowedHorses) 
        {
            HorseName = name;
            StartTime = startTime;
            AllowedHorses = allowedHorses;
            horses = new List<Horse>();
            UpdateAvailability();
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Race name cannot be empty.");
                }
                name = value;
            }
        }

        public DateTime StartTime
        {
            get => startTime;
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException($"Start time {value} is not valid.");
                }
                startTime = value;
            }
        }

        public int AllowedHorses
        {
            get => allowedHorses;
            set
            {
                if (allowedHorses < 3 && allowedHorses > 15)
                {
                    throw new ArgumentException("The number of allowed horses needs to be between 3 and 15 inclusive.");
                }
                allowedHorses = value;
            }
        }

        public List<Horse> Horses => horses;

        public bool Availability => availability;

        private void UpdateAvailability()
        {
            availability = horses.Count < allowedHorses;
        }

        public bool AddHorse(Horse horse)
        {
            if (IsRaceFull())
            {
                throw new ArgumentException("The race is already full.");
            }

            if (horses.Contains(horse))
            {
                throw new ArgumentException("The horse is already in the race.");
            }

            UpdateAvailability();
            return AddHorse(horse);
        }

        public bool RemoveHorse(Horse horse)
        {
            if (!horses.Any())
            {
                throw new ArgumentException("The race is empty");
            }

            if (!horses.Contains(horse))
            {
                throw new ArgumentException("The horse is not in the race.");
            }

            UpdateAvailability();
            return RemoveHorse(horse);
        }

        public bool IsRaceFull()
        {
            return horses.Count >= allowedHorses;
        }

        public bool IsRaceEmpty()
        {
            return horses.Count == 0;
        }

        public override string ToString()
        {
            string raceDetails = $"Race: {Name}, Start Time: {StartTime.ToShortTimeString()}, Allowed Horses: {allowedHorses}, Current Horses: {horses.Count}\nHorses:";
            foreach (var horse in horses)
            {
                raceDetails += $"\n  {horse}";
            }
            return raceDetails;
        }
    }
}
