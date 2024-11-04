using Horse_Race_App.objects;
using Horse_Race_App.utils;

namespace Horse_Race_App.people
{
    public class HorseOwner
    {
        public void EnterHorseInRace()
        {
            Console.WriteLine("Please enter the horse's information in the format: name, birth date (MM/dd/yyyy), horse ID");
            string[] values = Console.ReadLine().Split(',');

            if (values.Length != 3)
            {
                Console.WriteLine("Invalid input format. Please provide exactly three values separated by commas.");
                return;
            }

            string horseName = values[0].Trim();
            string birthDateStr = values[1].Trim();
            string horseId = values[2].Trim();

            try
            {
                DateTime birthDate = DateTime.Parse(birthDateStr);

                if (Horse.ValidateHorseId(horseId, FileUtils.DataToHorses()) &&
                    Horse.ValidateHorseName(horseName) &&
                    Horse.ValidateHorseAge(birthDate))
                {
                    Horse newHorse = new Horse(horseName, birthDate, horseId);
                    FileUtils.WriteNewHorse(newHorse);
                    Console.WriteLine("Your horse was successfully added to the system.");
                }
                else
                {
                    Console.WriteLine("Invalid input values. Ensure name is a string, date is in MM/dd/yyyy format, and ID is in format XXX999999999.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date format. Please enter the date in MM/dd/yyyy format.");
            }
        }

        public void RegisterHorse()
        {
            Console.WriteLine("Please provide your horse's name:");
            string inputName = Console.ReadLine();

            Horse importantHorse = null;
            List<Horse> horses = FileUtils.DataToHorses();

            foreach (var horse in horses)
            {
                if (horse.HorseName.Equals(inputName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Your horse was found. Which race event would you like to assign him to?");
                    importantHorse = horse;
                    break;
                }
            }

            if (importantHorse == null)
            {
                Console.WriteLine("Your horse is not in the system. Please add the horse first.");
                return;
            }

            List<RaceEvents> events = FileUtils.ReadRaceEvents();
            if (events.Count == 0)
            {
                Console.WriteLine("No upcoming events are available.");
                return;
            }

            Console.WriteLine("Upcoming events:");
            for (int i = 0; i < events.Count; i++)
            {
                var upcomingEvent = events[i];
                Console.WriteLine($"{i + 1}. Name: {upcomingEvent.EventName}, Location: {upcomingEvent.Location}, Date: {upcomingEvent.StartDate:dd-MM-yyyy}");
            }

            Console.Write("Select an event number: ");
            if (!int.TryParse(Console.ReadLine(), out int eventChoice) || eventChoice <= 0 || eventChoice > events.Count)
            {
                Console.WriteLine("Invalid event choice. Returning to main menu...");
                return;
            }

            RaceEvents chosenEvent = events[eventChoice - 1];
            Console.WriteLine("Which race would you like to assign the horse to?");
            
            for (int i = 0; i < chosenEvent.Races.Count; i++)
            {
                var race = chosenEvent.Races[i];
                int availableSpace = race.AllowedHorses - race.Horses.Count;
                Console.WriteLine($"{i + 1}. Race: {race.Name}, Time: {race.StartTime:hh\\:mm}, Available Spots: {availableSpace}");
            }
            
            Console.Write("Select a race number: ");
            if (!int.TryParse(Console.ReadLine(), out int raceChoice) || raceChoice <= 0 || raceChoice > chosenEvent.Races.Count)
            {
                Console.WriteLine("Invalid race choice. Returning to main menu...");
                return;
            }

            Race chosenRace = chosenEvent.Races[raceChoice - 1];

            if (chosenRace.AddHorse(importantHorse))
            {
                Console.WriteLine($"{importantHorse.HorseName} was successfully added to the race {chosenRace.Name}.");
            }
            else
            { 
                Console.WriteLine("Horse could not be added; they might already be registered or the race is full.");
            }
        }
    }
}