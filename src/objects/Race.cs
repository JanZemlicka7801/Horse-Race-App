namespace Horse_Race_App.objects
{
    public class Race
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public List<Horse> Horses { get; set; }
        public int AllowedHorses { get; set; }
        public bool Availability { get; set; }

        public Race(string raceName, DateTime raceStart, int raceAllowedHorses)
        {
            Horses = new List<Horse>();
            Availability = true;

            if (!ValidateRaceName(raceName) || !ValidateStartTime(raceStart) ||
                !ValidateRaceAllowedHorses(raceAllowedHorses))
            {
                return; //the input is valid so the Race won't be created
            }
            
            Name = raceName;
            StartTime = raceStart;
            AllowedHorses = raceAllowedHorses;
        }

        private static bool ValidateRaceAllowedHorses(int raceAllowedHorses)
        {
            if (raceAllowedHorses >= 3 && raceAllowedHorses <= 15) return true;
            Console.WriteLine("Race allowed horses are between 3 and 15.");
            return false;
        }

        private static bool ValidateStartTime(DateTime raceStart)
        {
            if (raceStart > DateTime.Now) return true;
            Console.WriteLine("Start time cannot be in the past.");
            return false;
        }
        
        private static bool ValidateRaceName(string raceName)
        {
            if (!string.IsNullOrEmpty(raceName)) return true;
            Console.WriteLine("Race name cannot be null or empty.");
            return false;
        }

        public bool RemoveHorse(Horse horse)
        {
            if (!Horses.Contains(horse)){ Console.WriteLine("Horse is not in the race"); return false;}
            if (RaceIsEmpty()){ Console.WriteLine("The race is already empty."); return false;}
            Horses.Remove(horse);
            return true;
        }
        
        public bool AddHorse(Horse horse)
        {
            if (Horses.Contains(horse)){ Console.WriteLine("Horse is already added."); return false;}
            if (RaceIsFull()){ Console.WriteLine("The race is already full."); return false;}
            Horses.Add(horse);
            return true;
        }

        public bool RaceIsFull()
        {
            return Horses.Count >= AllowedHorses;
        }
        
        public bool RaceIsEmpty()
        {
            return Horses.Count == 0;
        }

        public bool IsAvailabile()
        {
            return !RaceIsFull();
        }
        
        public override string ToString()
        {
            string raceDetails = $"Race: {Name}, Start Time: {StartTime.ToShortTimeString()}, Allowed Horses: {AllowedHorses}, Current Horses: {Horses.Count}\nHorses:";
            if (Horses.Count == 0)
            {
                raceDetails += "\n  No horses registered.";
            }
            else
            {
                foreach (var horse in Horses)
                {
                    raceDetails += $"\n  {horse}";
                }
            }
            return raceDetails;
        }
    }
}
