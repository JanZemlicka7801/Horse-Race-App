using Horse_Race_App.objects;
using Horse_Race_App.people;

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

        /**
         * Reads data from a text file line by line, split them and saves them as properties of the horse class.
         * Then adds them inside the list of string, DateTime and another string. In the end the list of data from
         * Horse.txt is outputted.
         */
        private static List<(string HorseId, DateTime BirthDate, string HorseName)> ReadHorseData()
        {
            var rawHorseData = new List<(string, DateTime, string)>();
            foreach (var line in File.ReadLines(HorseFilePath))
            {
                var parts = line.Split(',');

                if (parts.Length < 3)
                {
                    continue;
                }

                // Acceses the second element after trimming
                var horseId = parts[0].Split(':')[1].Trim();
                var birthDateString = parts[1].Split(':')[1].Trim();
                var horseName = parts[2].Split(':')[1].Trim();

                // resource: https://stackoverflow.com/questions/11999912/datetime-tryparseexact-rejecting-valid-formats
                if (DateTime.TryParseExact(
                        // the string that will be parsed
                        birthDateString, 
                        // provides the exact format for the parsing
                        "MM/dd/yyyy",
                        // passing culture specific formatting, null is default
                        null,
                        // won't display any additional adjustments like UTC
                        System.Globalization.DateTimeStyles.None, 
                        // if everything will pass then it will be stored inside the birthDate
                        out var birthDate))
                {
                    rawHorseData.Add((horseId, birthDate, horseName));
                }
            }

            return rawHorseData;
        }
        
        /*
         * Read races from Race.txt, by separating those data the function then creates race objects and stores
         * them inside a list. 
         */
        private static List<Race> ReadRaces()
        {
            var races = new List<Race>();

            foreach (var line in File.ReadLines(RaceFilePath))
            {
                try
                {
                    var parts = line.Split(',');

                    if (parts.Length < 4)
                    {
                        // Skips the loop.
                        continue;
                    }

                    string name = parts[0].Split(':')[1].Trim();
                    
                    if (!TimeSpan.TryParse(parts[1].Split(':')[1], out TimeSpan startTime))
                    {
                        continue;
                    }

                    var horseIDsPart = parts[2].Split('(')[1].TrimEnd(')');
                    var horseIDs = new List<string>();
                    foreach (var id in horseIDsPart.Split(','))
                    {
                        horseIDs.Add(id.Trim());
                    }

                    var allHorses = DataToHorses();
                    var raceHorses = new List<Horse>();

                    foreach (var horse in allHorses)
                    {
                        if (horseIDs.Contains(horse.HorseId))
                        {
                            raceHorses.Add(horse);
                        }
                    }

                    if (!int.TryParse(parts[3].Split(':')[1], out int allowedHorses))
                    {
                        continue;
                    }

                    races.Add(new Race(name, startTime, raceHorses, allowedHorses));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing race: {ex.Message}");
                }
            }

            return races;
        }

        /*
         * Reads all race events from a RaceEvent.txt and using splitting and trimming lines will extract data and parses them
         * to correct data types, which will allow to create RaceEvent objects and store them inside a list.
         */
        public static List<RaceEvents> ReadRaceEvents()
        {
            var raceEvents = new List<RaceEvents>();

            foreach (var line in File.ReadLines(RaceEventFilePath))
            {
                try
                {
                    var parts = line.Split(',');

                    if (parts.Length < 5)
                    {
                        continue;
                    }

                    var name = parts[0].Split(':')[1].Trim();
                    var location = parts[1].Split(':')[1].Trim();

                    DateTime date;
                    if (!DateTime.TryParse(parts[2].Split(':')[1], out date))
                    {
                        continue;
                    }

                    var raceNamesString = parts[3].Split('(')[1].TrimEnd(')');
                    var raceNames = new List<string>();
                    foreach (var raceName in raceNamesString.Split(','))
                    {
                        raceNames.Add(raceName.Trim());
                    }

                    var numberOfRacesString = parts[4].Split(':')[1].Trim('(', ')');
                    int numberOfRaces;
                    if (!int.TryParse(numberOfRacesString, out numberOfRaces))
                    {
                        continue;
                    }

                    var listOfRaces = ReadRaces();
                    var eventRaces = new List<Race>();

                    foreach (var race in listOfRaces)
                    {
                        if (raceNames.Contains(race.Name))
                        {
                            eventRaces.Add(race);
                        }
                    }

                    raceEvents.Add(new RaceEvents(name, location, date, eventRaces, numberOfRaces));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing race event: {ex.Message}");
                }
            }

            return raceEvents;
        }
        
        /*
         * Retrieves past race events from RaceEvents.txt and stores them inside a list.
         */
        public static List<RaceEvents> GetPastRaceEventsList()
        {
            var pastEvents = new List<RaceEvents>();
            var allEvents = ReadRaceEvents();

            foreach (var raceEvent in allEvents)
            {
                if (raceEvent.StartDate < DateTime.Today)
                {
                    pastEvents.Add(raceEvent);
                }
            }

            return pastEvents;
        }

        /*
         * From provided list of Race Events it will filter races assigned to the race event.
         */
        public static List<Race> GetAllRacesFromListOfEvents(List<RaceEvents> raceEvents)
        {
            var allRaces = new List<Race>();

            foreach (var raceEvent in raceEvents)
            {
                if (raceEvent.Races != null)
                {
                    allRaces.AddRange(raceEvent.Races);
                }
            }

            return allRaces;
        }
        
        /*
         * Transforms a list of raw data from Horse.txt to objects Horse, with assigned true to skip validation.
         */
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
        
        /*
        public static void DeleteHorse(string horseId)
        {
            var remainingHorses = File.ReadLines(HorseFilePath).Where(line => !line.Contains(horseId)).ToList();
            
            File.WriteAllLines(HorseFilePath,remainingHorses);
        }
        */
        
        /**
         * Compares stored race events to a list of race events for deletion and just rewrite the text file without
         * those assigned for deletion.
         */
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
        
        /**
         * Compares stored races to a list of races for deletion and just rewrite the text file without
         * those assigned for deletion.
         */
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
        
        /**
 * Writes a passed race event into RaceEvent.txt using a stream writer.
 */
        public static void WriteNewRaceEvent(RaceEvents raceEvent)
        {
            // Use append: true to avoid overwriting
            using (StreamWriter writer = new StreamWriter(RaceEventFilePath, append: true)) 
            {
                var raceNames = new List<string>();
                foreach (var race in raceEvent.Races)
                {
                    raceNames.Add(race.Name);
                }
        
                string raceNamesString = string.Join(",", raceNames);
                string eventLine = $"Name:{raceEvent.EventName},Location:{raceEvent.Location},StartDate:{raceEvent.StartDate:MM/dd/yyyy},Races:({raceNamesString}),NumberOfRaces:({raceEvent.NumberOfRaces})";

                writer.WriteLine(eventLine);
            }
        }

        /**
         * Writes a passed race into Race.txt using a stream writer.
         */
        private static void WriteNewRace(Race race)
        {
            // Use append: true to avoid overwriting
            using (StreamWriter writer = new StreamWriter(RaceFilePath, append: true)) 
            {
                var horseNames = new List<string>();
                foreach (var horse in race.Horses)
                {
                    horseNames.Add(horse.HorseName);
                }

                string horseNamesString = string.Join(",", horseNames);
                string raceLine = $"Name:{race.Name},StartTime:{race.StartTime},HorsesIDs({horseNamesString}),AllowedHorses:{race.AllowedHorses}";

                writer.WriteLine(raceLine);
            }
        }

        /**
         * Writes a passed horse into Horse.txt using a stream writer.
         */
        public static void WriteNewHorse(Horse horse)
        {
            using (StreamWriter writer = new StreamWriter(HorseFilePath, append: true))
            {
                string horseLine = $"HorseId:{horse.HorseId},BirthDate:{horse.BirthDate:MM/dd/yyyy},HorseName:{horse.HorseName}";
                writer.WriteLine(horseLine);
            }
        }

        /**
         * Writes races from a list to text file.
         */
        public static void WriteWholeNewRaces(List<Race> list)
        {
            foreach (var race in list)
            {
                WriteNewRace(race);
            }
        }

        /**
         * Writes race events from a list to text file.
         */
        public static void WriteWholeNewEventRaces(List<RaceEvents> list)
        {
            foreach (var raceEvent in list)
            {
                WriteNewRaceEvent(raceEvent);
            }
        }

    }
}
