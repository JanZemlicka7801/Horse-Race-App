using Horse_Race_App.objects;
using Horse_Race_App.people;
using Horse_Race_App.utils;

namespace Horse_Race_App
{
    abstract class Program
    //TODO: Forgot to validate all of the horses stored inside the file as they can get older.
    {
        static void Main()
        {
            List<RaceEvents> pastEventsList = FileUtils.GetPastRaceEventsList();
            FileUtils.DeleteRaceEventsFromFile(pastEventsList);
            List<Race> pastRaces = FileUtils.GetAllRacesFromListOfEvents(pastEventsList);
            FileUtils.DeleteRacesFromFile(pastRaces);

            //TODO: storage of users, clean up the code, comment it out, explain complex functions

            while (true)
            {
                Console.WriteLine("Please select an option you want to continue:\n" +
                                  "1. Log in\n" +
                                  "2. Register\n" +
                                  "3. Exit\n");

                string? choice = Console.ReadLine();
                UserRole? userRole;

                switch (choice)
                {
                    case "1":
                        userRole = Menus.Login();
                        break;

                    case "2":
                        userRole = Menus.Register();
                        break;

                    case "3":
                        Console.WriteLine("Thanks for using our program.");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        continue;
                }

                if (userRole.HasValue)
                {
                    switch (userRole.Value)
                    {
                        case UserRole.HorseOwner:
                            Menus.horseOwnerMenu(new HorseOwner());
                            break;

                        case UserRole.Racegoer:
                            Menus.raceGoerMenu(new Racegoer());
                            break;

                        case UserRole.RaceOwner:
                            Menus.racecourseManagerMenu(new RacecourseManager());
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Login or registration failed. Please try again.");
                }
            }

        }
    }
}