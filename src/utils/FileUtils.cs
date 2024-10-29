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
    }
}
