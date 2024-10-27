using Horse_Race_App.objects;

namespace Horse_Race_App.people
{
    internal class RacecourseManager
    {
        public List<RaceEvents> Events {  get; set; }

        //main list that holds all race events
        public RacecourseManager()
        {
            Events = new List<RaceEvents>();
        }

        //function for creating a race event and log it inside the manager
        public RaceEvents CreateRaceEvent(string eventName, string location, int numberOfRaces)
        {
            foreach (var raceEvent in Events)
            {
                if (raceEvent.EventName.Equals(eventName))
                {
                    Console.WriteLine($"Event {eventName} is already in the system.");
                }
            }

            var RaceEvent = new RaceEvents(eventName, location, numberOfRaces);
            Events.Add(RaceEvent);
            return RaceEvent;
        }

        //function for deleting the race event from the manager
        public bool DeleteRaceEvent(string eventName)
        {
            foreach (var raceEvent in Events)
            {
                if (raceEvent.EventName.Equals(eventName))
                {
                    Events.Remove(raceEvent);
                    return true;
                }
            }
            Console.WriteLine($"Event {eventName} is not in the system. In this case, the race event won't be deleted.");
            return false;
        }
        
        //function for adding races to the event
        public void AddRaces(string eventName)
        {
            //loop through events to find the right one using case ignorance
            var raceEvent = Events.FirstOrDefault(e => e.EventName.Equals(eventName, StringComparison.OrdinalIgnoreCase));
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
                    $"In following format: name,date(yyyy-mm-dd),number of allowed horses\n" +
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
                DateTime date;
                int numberOfHorses;
                    
                try
                {
                    raceName = detailsSplit[0];
                    date = DateTime.Parse(detailsSplit[1]);
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
                
                if(date < DateTime.Now){
                    Console.WriteLine("Invalid date. Please enter a future date in the format yyyy-mm-dd.");
                    continue;
                }
                
                if (numberOfHorses < 3 || numberOfHorses > 15)
                {
                    Console.WriteLine("Invalid number of allowed horses. It must be between 3 and 15.");
                    continue;
                }
                
                Race race = new Race(raceName, date, numberOfHorses);
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
        
        //function for adding whole list of horses for each race
        //function for adding horse manually
    }
}
