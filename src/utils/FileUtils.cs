using Horse_Race_App.objects;

namespace Horse_Race_App.utils
{
    public static class FileUtils
    {
        private const string FilePath = "data.txt";

        public static List<RaceEvents> RetrieveRaceEvents()
        {
            var events = new List<RaceEvents>();

            if (!File.Exists(FilePath))
                return events;

            var lines = File.ReadAllLines(FilePath);
            RaceEvents currentEvent = null;
            Race currentRace = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("Event:"))
                {
                    var details = line.Replace("Event:", "").Split(',');
                    var eventName = details[0].Trim();
                    var location = details[1].Trim();
                    currentEvent = new RaceEvents(eventName, location);
                    events.Add(currentEvent);
                }
                else if (line.StartsWith("Race:"))
                {
                    var details = line.Replace("Race:", "").Split(',');
                    var raceName = details[0].Trim();
                    var startTime = DateTime.Parse(details[1].Trim());
                    var allowedHorses = int.Parse(details[2].Trim());
                    currentRace = new Race(raceName, startTime, allowedHorses);
                    currentEvent?.Races.Add(currentRace);
                }
                else if (line.StartsWith("Horse:"))
                {
                    var details = line.Replace("Horse:", "").Split(',');
                    var horseName = details[0].Trim();
                    var birthDate = DateTime.Parse(details[1].Trim());
                    var horseId = details[2].Trim();
                    currentRace?.AddHorse(new Horse(horseName, birthDate, horseId));
                }
            }

            return events;
        }

        public static void writeEvents(List<RaceEvents> events)
        {
            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (var evt in events)
                {
                    foreach (var race in evt.Races)
                    {
                        if (race.Horses.Count < 3 || race.Horses.Count > 15)
                        {
                            throw new Exception($"Race '{race.Name}' in event '{evt.EventName}' must have between 3 and 15 horses. Current count: {race.Horses.Count}");
                        }

                        string horses = string.Join(", ", race.Horses.Select(h =>
                            $"{{id:{h.HorseId}, name:'{h.HorseName}', age:{h.BirthDate}}}"));

                        writer.WriteLine($"event: \"{evt.EventName}\", race: \"{race.Name}\", horses: \"[{horses}]\"");
                    }
                }
            }
        }
    }
}
