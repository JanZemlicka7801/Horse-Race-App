using Horse_Race_App.src.objects;
using Horse_Race_App.src.people;

class Program
{
    static void Main()
    {
        RacecourseManager manager = new RacecourseManager();
        HorseOwner owner = new HorseOwner();
        Racegoer visitor = new Racegoer();
        List<RaceEvent> raceEvent = manager.Events; 

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
                    if (raceEvent.Count == 0)
                    {
                        Console.WriteLine("There are no upcoming events to show.");
                    }
                    else
                    {
                        Racegoer.ViewUpcomingEvents(raceEvent);
                    }
                    break;

                case "3":

                    break;

                case "4": // quits the program
                    Console.WriteLine("Exiting the program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice please try it again.");
                    break;
            }
        }
    }
}