using Horse_Race_App.objects;

namespace Horse_Race_App.utils
{
    public static class FileUtils
    {
        private const string HorseFilePath =
            "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\Horse.txt";

        private const string RaceFilePath =
            "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\Race.txt";

        private const string RaceEventFilePath =
            "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\RaceEvent.txt";

        private static List<(string HorseId, DateTime BirthDate, string HorseName)> ReadHorseData()
        {
            var rawHorseData = new List<(string, DateTime, string)>();
            foreach (var line in File.ReadLines(HorseFilePath))
            {
                var parts = line.Split(',');
                string horseId = parts[0].Split(':')[1];
                DateTime birthDate = DateTime.Parse(parts[1].Split(':')[1]);
                string horseName = parts[2].Split(':')[1];
                rawHorseData.Add((horseId, birthDate, horseName));
            }

            return rawHorseData;
        }
        
        public static List<Horse> DataToHorses()
        {
            var horses = new List<Horse>();
            var rawHorseData = ReadHorseData();
            foreach (var (horseId, birthDate, horseName) in rawHorseData)
            {
                var horse = new Horse(horseName, birthDate, horseId, skipValidation: true);
                horses.Add(horse);
            }
            return horses;
        }
        
        public static void DeleteHorse(string horseId)
        {
            var remainingHorses = File.ReadLines(HorseFilePath).Where(line => !line.Contains(horseId)).ToList();
            
            File.WriteAllLines(HorseFilePath,remainingHorses);
        }
        
        public static void DeleteRaceEventsFromFile(List<RaceEvents> eventsToDelete)
        {
            var allEvents = File.ReadAllLines(RaceEventFilePath).ToList();
            var remainingEvents = new List<string>();

            foreach (var line in allEvents)
            {
                string eventName = line.Split(',')[0].Split(':')[1].Trim();
                bool shouldDelete = false;

                foreach (var eventToDelete in eventsToDelete)
                {
                    if (eventToDelete.EventName.Equals(eventName, StringComparison.Ordinal))
                    {
                        shouldDelete = true;
                        break;
                    }
                }
                
                if (!shouldDelete)
                {
                    remainingEvents.Add(line);
                }
            }

            File.WriteAllLines(RaceEventFilePath, remainingEvents);
        }
        
        public static void DeleteRacesFromFile(List<Race> racesToDelete)
        {
            var allRaces = File.ReadAllLines(RaceFilePath).ToList();
            var remainingRaces = new List<string>();

            if (allRaces.Count == 0)
            {
                return;
            }

            foreach (string line in allRaces)
            {
                string raceName = line.Split(',')[0].Split(':')[1].Trim();
                bool shouldDelete = false;

                foreach (var race in racesToDelete)
                {
                    if (race.Name == raceName)
                    {
                        shouldDelete = true;
                        break;
                    }
                }

                if (!shouldDelete)
                {
                    remainingRaces.Add(line);
                }
            }
            
            File.WriteAllLines(RaceFilePath, remainingRaces);
        }
        
        public static void WriteNewRaceEvent(RaceEvents raceEvent)
        {
            using (StreamWriter writer = new StreamWriter(RaceEventFilePath))
            {
                string raceNames = string.Join(",", raceEvent.Races.Select(r => r.Name));
                string eventLine = $"Name:{raceEvent.EventName},Location:{raceEvent.Location},StartDate:{raceEvent.StartDate:MM/dd/yyyy},Races:({raceNames}),NumberOfRaces:({raceEvent.NumberOfRaces})";
                
                writer.WriteLine(eventLine);
            }
        }

        public static void WriteNewRace(Race race)
        {
            using (StreamWriter writer = new StreamWriter(RaceFilePath))
            {
                string horseNames = string.Join(",", race.Horses.Select(r => r.HorseName));
                string raceLine = $"Name:{race.Name},StartTime:{race.StartTime},HorsesIDs({horseNames}),AllowedHorses:{race.AllowedHorses}";
                
                writer.WriteLine(raceLine);
            }
        }

        public static void WriteNewHorse(Horse horse)
        {
            using (StreamWriter writer = new StreamWriter(HorseFilePath))
            {
                string horseLine = $"HorseId:{horse.HorseId},BirthDate:{horse.BirthDate},HorseName:{horse.HorseName}";
                writer.WriteLine(horseLine);
            }
        }

        public static void WriteWholeNewHorses(List<Horse> list)
        {
            foreach (var horse in list)
            {
                WriteNewHorse(horse);
            }
        }

        public static void WriteWholeNewRaces(List<Race> list)
        {
            foreach (var race in list)
            {
                WriteNewRace(race);
            }
        }

        public static void WriteWholeNewEventRaces(List<RaceEvents> list)
        {
            foreach (var raceEvent in list)
            {
                WriteNewRaceEvent(raceEvent);
            }
        }

    }
}
