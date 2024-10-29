using Horse_Race_App.objects;
using Horse_Race_App.people;

namespace Horse_Race_App.utils
{
    public static class FileUtils
    {
        public static List<Horse> ReadHorses()
        {
            var horses = new List<Horse>();

            foreach (var line in File.ReadLines("C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\Horse.txt"))
            {
                //no validation as the program will write all information inside the file
                var parts = line.Split(',');
                string horseId = parts[0].Split(':')[1];
                DateTime birthDate = DateTime.Parse(parts[1].Split(':')[1]);
                string horseName = parts[2].Split(':')[1];
                horses.Add(new Horse(horseName, birthDate, horseId));
            }
            
            return horses;
        }

        public static List<Race> ReadRaces()
        {
            var races = new List<Race>();
            foreach (var line in File.ReadLines("C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\Race.txt"))
            {
                var parts = line.Split(',');
                string name = parts[0].Split(':')[1];
                TimeSpan startTime = TimeSpan.Parse(parts[1].Split(':')[1]);
                var horsesIDs = parts[2].Split('(')[1].TrimEnd(')').Split(',').ToList();
                int allowedHorses = int.Parse(parts[3].Split(':')[1]);
                List<Horse> listOfHorses = ReadHorses();
                foreach (var horse in listOfHorses)
                {
                    if (!horsesIDs.Contains(horse.HorseId))
                    {
                        listOfHorses.Remove(horse);
                    }
                }
                races.Add(new Race(name, startTime, listOfHorses, allowedHorses));
            }
            return races;
        }

        public static List<RaceEvents> ReadRaceEvents()
        {
            var raceEvents = new List<RaceEvents>();
            foreach (var line in File.ReadLines(
                         "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\RaceEvent.txt"))
            {
                var parts = line.Split(',');
                string name = parts[0].Split(':')[1];
                string location = parts[1].Split(':')[1];
                DateTime date = DateTime.Parse(parts[2].Split(':')[1]);
                var raceNames = parts[3].Split('(')[1].TrimEnd(')').Split(',').ToList();
                int numberOfRaces = Int32.Parse(parts[4].Split(':')[1]);
                List<Race> listOfRaces = ReadRaces();
                foreach (var race in listOfRaces)
                {
                    if (!raceNames.Contains(race.Name))
                    {
                        listOfRaces.Remove(race);
                    }
                }
                raceEvents.Add(new RaceEvents(name, location, date, listOfRaces, numberOfRaces));
            }
            return raceEvents;
        }

        public static void WriteNewRaceEvent(RaceEvents raceEvent)
        {
            using (StreamWriter writer =
                   new StreamWriter(
                       "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\RaceEvent.txt"))
            {
                string raceNames = string.Join(",", raceEvent.Races.Select(r => r.Name));
                string eventLine = $"Name:{raceEvent.EventName},Location:{raceEvent.Location},StartDate:{raceEvent.StartDate:MM/dd/yyyy},Races:({raceNames}),NumberOfRaces:({raceEvent.NumberOfRaces})";
                
                writer.WriteLine(eventLine);
            }
        }

        public static void WriteNewRace(Race race)
        {
            using (StreamWriter writer =
                   new StreamWriter(
                       "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\Race.txt"))
            {
                string horseNames = string.Join(",", race.Horses.Select(r => r.HorseName));
                string raceLine =
                    $"Name:{race.Name},StartTime:{race.StartTime},HorsesIDs({horseNames}),AllowedHorses:{race.AllowedHorses}";
                
                writer.WriteLine(raceLine);
            }
        }

        public static void WriteNewHorse(Horse horse)
        {
            using (StreamWriter writer =
                   new StreamWriter(
                       "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\Horse.txt"))
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
            foreach (var Race in list)
            {
                WriteNewRace(Race);
            }
        }

        public static void WriteWholeNewEventRaces(List<RaceEvents> list)
        {
            foreach (var RaceEvent in list)
            {
                WriteNewRaceEvent(RaceEvent);
            }
        }
    }
}
