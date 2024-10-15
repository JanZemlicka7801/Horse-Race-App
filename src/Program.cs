using System.Text.RegularExpressions;
using Horse_Race_App.objects;
using Horse_Race_App.people;
using Horse_Race_App.utils;

namespace Horse_Race_App
{
    class Program
{
    static void Main()
    {
        RacecourseManager manager = new RacecourseManager();
        HorseOwner owner = new HorseOwner();
        Racegoer visitor = new Racegoer();

        List<RaceEvents> raceEvents = FileUtils.RetrieveRaceEvents();
        manager.Events.AddRange(raceEvents);

        Console.WriteLine("Hello and Welcome to a Zemlicka's Horse Derby. Please one the options of how you wanna interact\n" +
                          "as with the system.\n");
        while (true)
        {
            Console.WriteLine(
                "1. I want to sign my horse to an event.\n" +
                "2. I want to see a race.\n" +
                "3. I am a manager and want to interact with the system.\n" +
                "4. I want to quit the program.");

            string answer = Console.ReadLine();

            switch (answer)
            {
                case "1":
                    
                    break;

                case "2": //view upcoming races
                    if (manager.Events.Count == 0)
                    {
                        Console.WriteLine("There are no upcoming events to show.");
                    }
                    else
                    {
                        Racegoer.ViewUpcomingEvents(manager.Events);
                    }
                    break;

                case "3":
                        Console.WriteLine("1. Create a new race event.");
                        Console.WriteLine("2. Add a race to an event.");
                        string managerChoice = Console.ReadLine();

                        if (managerChoice == "1")
                        {
                            Console.WriteLine("Enter event name:");
                            string eventName = Console.ReadLine();
                            Console.WriteLine("Enter race location:");
                            string location = Console.ReadLine();
                            manager.CreateRaceEvent(eventName, location);
                            Console.WriteLine("The race event has been created.");
                        }
                        else if (managerChoice == "2")
                        {
                            if (manager.Events.Count == 0)
                            {
                                Console.WriteLine("There are no upcoming events to show. Please create one.");
                            }
                            else
                            {
                                Console.WriteLine("Select the race event you want to add race to:");
                                for (int i = 0; i < manager.Events.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {manager.Events[i].EventName}");
                                }
                                int selectedRaceEvent = int.Parse(Console.ReadLine()) - 1;
                                
                                Console.WriteLine("Enter race name:");
                                string raceName = Console.ReadLine();
                                Console.WriteLine("Enter start time (yyyy-mm-dd):)");
                                DateTime startTime = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the maximum number of horse for the event:");
                                int allowedHorses = int.Parse(Console.ReadLine());
                                
                                Race newRace = new Race(raceName, startTime, allowedHorses);
                                manager.AddRaceToEvent(manager.Events[selectedRaceEvent], newRace);
                                Console.WriteLine("The race event has been added.");
                            }
                        }
                    break;
                case "4": // quits the program
                    FileUtils.SaveRaceEvents(manager.Events);
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice please try it again.");
                    break;
            }
        }
    }
}
}
