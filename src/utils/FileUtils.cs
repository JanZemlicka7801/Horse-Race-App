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

        public static void SaveRaceEvents(List<RaceEvents> events)
        {
            var lines = new List<string>();

            foreach (var raceEvent in events)
            {
                lines.Add($"Event: {raceEvent.EventName}, {raceEvent.Location}");

                foreach (var race in raceEvent.Races)
                {
                    lines.Add($"Race: {race.Name}, {race.StartTime}, {race.AllowedHorses}");

                    foreach (var horse in race.Horses)
                    {
                        lines.Add($"Horse: {horse.HorseName}, {horse.BirthDate.ToShortDateString()}, {horse.HorseId}");
                    }
                }
            }

            File.WriteAllLines(FilePath, lines);
        }
    }
}
