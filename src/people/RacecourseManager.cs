using Horse_Race_App.objects;
using Horse_Race_App.utils;

namespace Horse_Race_App.people
{
    public class RacecourseManager
    {
        public List<RaceEvents> Events {  get; set; }

        //main list that holds all race events
        public RacecourseManager()
        {
            Events = new List<RaceEvents>();
        }

        //function for creating a race event and log it inside the manager
        public RaceEvents CreateRaceEvent(string eventName, string location, DateTime date, List<Race> list, int numberOfRaces)
        {
            foreach (var raceEvent in FileUtils.ReadRaceEvents())
            {
                if (raceEvent.EventName.Equals(eventName))
                {
                    Console.WriteLine($"Event {eventName} is already in the system.");
                    return null;
                }
            }

            var RaceEvent = new RaceEvents(eventName, location, date, list, numberOfRaces);
            Events.Add(RaceEvent);
            return RaceEvent;
        }

        //function for deleting the race event from the manager
        public bool DeleteRaceEvent(string eventName)
        {
            RaceEvents raceToDelete = FindRaceEventByName(eventName);
            Events.Remove(raceToDelete);
            Console.WriteLine($"Event {eventName} is not in the system. In this case, the race event won't be deleted.");
            return false;
        }
        
        private RaceEvents FindRaceEventByName(string eventName)
        {
            foreach (var raceEvent in Events)
            {
                    if (raceEvent.EventName.Equals(eventName, StringComparison.OrdinalIgnoreCase))
                    {
                        return raceEvent;
                    }
            }
            return null;
        }
        
        //function for adding races to the event
        public void AddRaces(string eventName)
        {
            //loop through events to find the right one using case ignorance
            var raceEvent = FindRaceEventByName(eventName);
            if (raceEvent == null)
            {
                Console.WriteLine($"Event '{eventName}' not found.");
                return;
            }

            int numberOfRaces;
            try
            {
                Console.WriteLine($"Please enter how many races would you like to add?");
                numberOfRaces = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input try it again.");
                return;
            }
            
            //create those races
            for (var i = 0; i < numberOfRaces; i++)
            {
                Console.WriteLine(
                    $"Please enter name for the {i + 1}. race event, start date and number of allowed horses for this race event.\n" +
                    $"In following format: name,time (hh:mm),number of allowed horses\n" +
                    $"All variables need to be seperated by a coma.");
                string details = Console.ReadLine();
                string[] detailsSplit = details.Split(',');
                
                //if there are no 3 inputs then error
                if (detailsSplit.Length != 3)
                {
                    Console.WriteLine("Invalid input try to create next race.");
                    continue;
                }

                string raceName;
                TimeSpan date;
                int numberOfHorses;
                    
                try
                {
                    raceName = detailsSplit[0];
                    date = TimeSpan.Parse(detailsSplit[1]);
                    numberOfHorses = Convert.ToInt32(detailsSplit[2]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid inputs try to create next race.");
                    continue;
                }
                
                //validation of component, but could use already created validators
                if (string.IsNullOrEmpty(raceName))
                {
                    Console.WriteLine("Race name cannot be empty.");
                    continue;
                }
                
                if (numberOfHorses < 3 || numberOfHorses > 15)
                {
                    Console.WriteLine("Invalid number of allowed horses. It must be between 3 and 15.");
                    continue;
                }
                
                Race race = new Race(raceName, date, new List<Horse>(), numberOfHorses);
                raceEvent.AddRace(race);
                Console.WriteLine($"Race '{raceName}' has been added to event '{eventName}'.");
            }
        }
        
        //function for deleting races from the event
        public void RemoveRaces(string eventName)
        {
            //find the race event
            var raceEvent = Events.FirstOrDefault(e => e.EventName.Equals(eventName, StringComparison.OrdinalIgnoreCase));
            if (raceEvent == null)
            {
                Console.WriteLine($"Event '{eventName}' not found.");
                return;
            }

            //check if there is anything to delete
            if (raceEvent.IsEmptyEvent())
            {
                Console.WriteLine($"No races available to delete in event '{eventName}'.");
                return;
            }
            
            //display possible deletions
            Console.WriteLine("Please write numbers of races that you want to delete.");
            string input = Console.ReadLine();
            string[] numbersSplit = input.Split(',');
            
            //try to delete races
            foreach (var selection in numbersSplit)
            {
                try
                {
                    int number = Convert.ToInt32(selection);
                    Race raceToRemove = raceEvent.Races[number];
                    raceEvent.RemoveRace(raceToRemove);
                    Console.WriteLine($"Race '{raceToRemove.Name}' has been deleted.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input... Selected option cannot be deleted.");
                }
            }
        }
        
        private Race FindRaceByName(string raceName)
        {
            foreach (var raceEvent in Events)
            {
                foreach (var race in raceEvent.Races)
                {
                    if (race.Name.Equals(raceName, StringComparison.OrdinalIgnoreCase))
                    {
                        return race;
                    }
                }
            }
            return null;
        }
        
        //function for adding whole list of horses for each race
        public void addHorsesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The specified file does not exist.");
                return;
            }

            Race race = null;

            foreach (var line in File.ReadLines(filePath))
            {
                var newLine = line.Trim();

                if (newLine.StartsWith("Race:", StringComparison.OrdinalIgnoreCase))
                {
                    string raceName = newLine.Substring("Race:".Length);
                    race = FindRaceByName(raceName);

                    if (race == null)
                    {
                        Console.WriteLine("Race was not found.");
                        continue;
                    }
                }else if (race != null && !string.IsNullOrEmpty(newLine))
                {
                    string[] horsesSplit = newLine.Split(',');

                    if (horsesSplit.Length != 3)
                    {
                        Console.WriteLine("Invalid input for horse entry.");
                        continue;
                    }
                    
                    string horseName = horsesSplit[0].Trim();
                    string birthDateStr = horsesSplit[1].Trim();
                    string horseId = horsesSplit[2].Trim();
                    
                    DateTime date = DateTime.Today;

                    try
                    {
                        date = DateTime.Parse(birthDateStr);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input for birth date.");
                    }
                    
                    Horse horse = new Horse(horseName, date, horseId);
                    if (race.AddHorse(horse))
                    {
                        Console.WriteLine($"Horse '{horseName}' added to race '{race.Name}'.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to add horse '{horseName}' to race '{race.Name}'. The race may be full or the horse may already be registered.");
                    }
                }
            }
        }
        
        //function for adding horse manually
        public void addSingleHorse()
        {
            Console.WriteLine("Please enter the name of the race.");
            string raceName = Console.ReadLine();
            
            Race race = FindRaceByName(raceName);

            if (race == null)
            {
                Console.WriteLine("Race was not found.");
                return;
            }
            
            Console.WriteLine("Please enter details of the horse seperated by comma ',' .");
            string[] horsesSplit = Console.ReadLine().Split(',');

            if (horsesSplit.Length != 3)
            {
                Console.WriteLine("Invalid input for horse entry.");
            }
            
            string horseName = horsesSplit[0].Trim();
            string birthDateStr = horsesSplit[1].Trim();
            string horseId = horsesSplit[2].Trim();
                    
            DateTime date = DateTime.Today;

            try
            {
                date = DateTime.Parse(birthDateStr);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input for birth date.");
            }
                    
            Horse horse = new Horse(horseName, date, horseId);
            if (race.AddHorse(horse))
            {
                Console.WriteLine($"Horse '{horseName}' added to race '{race.Name}'.");
            }
            else
            {
                Console.WriteLine($"Failed to add horse '{horseName}' to race '{race.Name}'. The race may be full or the horse may already be registered.");
            }
        }
    }
}
