using Horse_Race_App.objects;
using Horse_Race_App.people;

namespace Horse_Race_App.utils
{
    public class Menus
    {
        /**
         * Registers a new user account, prompting for username, password, and role.
         * Checks if the username already exists; if not, it writes the new user to User.txt.
         */
        public static UserRole? Register()
        {
            const string userFilePath = "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\User.txt";

            Console.WriteLine("Registering a new account. Please select your role:");
            var selectedRole = SelectUserRole();

            if (selectedRole.HasValue)
            {
                Console.Write("Enter a username: ");
                string username = Console.ReadLine();

                Console.Write("Enter a password: ");
                string password = Console.ReadLine();

                // Check if the username already exists by iterating over each line in the file
                bool usernameExists = false;
                foreach (var line in File.ReadAllLines(userFilePath))
                {
                    if (line.StartsWith($"Username:{username},"))
                    {
                        usernameExists = true;
                        break;
                    }
                }

                if (usernameExists)
                {
                    Console.WriteLine("Username already exists. Please choose a different username.");
                    return null;
                }


                // Save the new user details to User.txt
                string userRecord = $"Username:{username},Password:{password},Role:{selectedRole}";
                File.AppendAllText(userFilePath, userRecord + Environment.NewLine);
                Console.WriteLine("Registration successful.");
                return selectedRole.Value;
            }
            else
            {
                Console.WriteLine("Invalid role selection. Registration failed.");
                return null;
            }
        }

        /**
         * Authenticates a user based on username and password, returning their UserRole.
         * Reads User.txt to validate credentials and retrieve the user's role.
         */
        public static UserRole? Login()
        {
            const string userFilePath = "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\User.txt";

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            // Validate username and password by matching against User.txt records
            foreach (var line in File.ReadLines(userFilePath))
            {
                var parts = line.Split(',');
                var storedUsername = parts[0].Split(':')[1];
                var storedPassword = parts[1].Split(':')[1];
                var storedRole = parts[2].Split(':')[1];

                if (storedUsername == username && storedPassword == password)
                {
                    if (Enum.TryParse(storedRole, out UserRole role))
                    {
                        Console.WriteLine($"Login successful. Welcome, {role}.");
                        return role;
                    }
                }
            }

            Console.WriteLine("Invalid username or password.");
            return null;
        }
        
        /**
         * Prompts the user to select a role from the available UserRole enum values.
         * Returns the selected UserRole, or null if the selection is invalid.
         */
        private static UserRole? SelectUserRole()
        {
            Console.WriteLine("Select your role:");
            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                Console.WriteLine($"{(int)role + 1}. {role}");
            }

            while (true)
            {
                Console.Write("Enter the number corresponding to your role: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(UserRole), choice - 1))
                {
                    return (UserRole)(choice - 1);
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }
        }

        /**
         * Presents the racecourse manager menu, with options for creating events, adding races, uploading horses, and signing out.
         * Access functionality to methods within the RacecourseManager class.
         */
        public static void racecourseManagerMenu(RacecourseManager racecourseManager)
        {
            while (true)
            {
                Console.WriteLine("Please select one of the following options:\n" +
                                  "1. Create a new race event\n" +
                                  "2. Add races to an event\n" +
                                  "3. Upload a list of horses to a race\n" +
                                  "4. Sign out");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            racecourseManager.CreateNewRaceEvent();
                            break;

                        case 2:
                            racecourseManager.AddRacesToEvent();
                            break;

                        case 3:
                            racecourseManager.UploadHorsesToRace();
                            break;

                        case 4:
                            Console.WriteLine("Thank you for using the program. Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        /**
         * Displays the menu for a racegoer, allowing them to view upcoming events or sign out from the program.
         */
        public static void raceGoerMenu(Racegoer racegoer)
        {
            while (true)
            {
                List<RaceEvents> upcomingRaceEvents = FileUtils.ReadRaceEvents();

                Console.WriteLine("Please select one of the following options:\n" +
                                  "1. See upcoming events\n" +
                                  "2. Sign out");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            racegoer.ViewUpcomingEvents(upcomingRaceEvents);
                            break;

                        case 2:
                            Console.WriteLine("Thank you for using our program. Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }
        
        /**
         * Presents the menu for a horse owner, allowing them to add a horse to the system, register it in a race event, or sign out.
         */
        public static void horseOwnerMenu(HorseOwner horseOwner)
        {
            while (true)
            {
                Console.WriteLine("Please select one of the following options:\n" +
                                  "1. Add your horse to the system.\n" +
                                  "2. Register your horse in a race event.\n" +
                                  "3. Sign out");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            horseOwner.EnterHorseInRace();
                            break;

                        case 2:
                            horseOwner.RegisterHorse();
                            break;

                        case 3:
                            Console.WriteLine("Thank you for using our program. Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option (1, 2, or 3).");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value corresponding to your choice.");
                }
            }
        }
    }
}