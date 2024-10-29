namespace Horse_Race_App.objects
{
    public class Race
    {
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public List<Horse> Horses { get; set; }
        public int AllowedHorses { get; set; }

        public Race(string raceName, TimeSpan raceStart, List<Horse> list, int raceAllowedHorses)
        {
            if (!ValidateRaceName(raceName) ||
                !ValidateRaceAllowedHorses(raceAllowedHorses))
            {
                return; //the input is valid so the Race won't be created
            }
            
            Name = raceName;
            StartTime = raceStart;
            AllowedHorses = raceAllowedHorses;
            Horses = list;
        }

        private static bool ValidateRaceAllowedHorses(int raceAllowedHorses)
        {
            if (raceAllowedHorses >= 3 && raceAllowedHorses <= 15) return true;
            Console.WriteLine("Race allowed horses are between 3 and 15.");
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

        private bool RaceIsFull()
        {
            return Horses.Count >= AllowedHorses;
        }
        
        private bool RaceIsEmpty()
        {
            return Horses.Count == 0;
        }
        
        public override string ToString()
        {
            string raceDetails = $"Race: {Name}, Start Time: {StartTime}, Allowed Horses: {AllowedHorses}, Current Horses: {Horses.Count}\nHorses:";
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
