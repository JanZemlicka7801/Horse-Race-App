using Horse_Race_App.objects;
using Horse_Race_App.people;

class Program
{
    static void Main()
    {
        RacecourseManager manager = new RacecourseManager();
        HorseOwner owner = new HorseOwner();
        Racegoer visitor = new Racegoer();
        List<RaceEvents> raceEvent = manager.Events; 

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
                    Horse horse = new Horse("",new DateTime(0000,00,00), "");

                    if (raceEvent.Count == 0)
                    {
                        Console.WriteLine("There are no upcoming events to show.");
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine("Please select one event you want to assign your horse to.");
                            Console.WriteLine("Please enter the name of the horse:");
                            horse.HorseName = Console.ReadLine();

                            Console.WriteLine("Please enter the birthdate of the horse (yyyy-mm-dd):");
                            horse.BirthDate = DateTime.Parse(Console.ReadLine());

                            Console.WriteLine("Please enter the horse ID (3 uppercase letters followed by 9 digits):");
                            horse.HorseId = Console.ReadLine();


                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                            Console.WriteLine("Please try it again..");
                        }
                    }

                    
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