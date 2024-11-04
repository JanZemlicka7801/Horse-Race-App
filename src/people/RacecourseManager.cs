using System;
using System.Collections.Generic;
using System.IO;
using Horse_Race_App.objects;
using Horse_Race_App.utils;

namespace Horse_Race_App.people
{
    public class RacecourseManager
    {
        private List<RaceEvents> Events { get; set; }

        // Constructor for initializing the manager's event list from storage
        public RacecourseManager()
        {
            Events = FileUtils.ReadRaceEvents();
        }

        // Method to create and add a new race event
        public void CreateNewRaceEvent()
        {
            Console.WriteLine("Enter event details in the format: Name, Location, Date (MM/dd/yyyy), Number of Races");
            string[] details = Console.ReadLine()?.Split(',');

            if (details == null || details.Length != 4)
            {
                Console.WriteLine("Invalid input format.");
                return;
            }

            string eventName = details[0].Trim();
            string location = details[1].Trim();

            if (!DateTime.TryParseExact(details[2].Trim(), "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date) || date <= DateTime.Today)
            {
                Console.WriteLine("Invalid date entered. The date must be in the future.");
                return;
            }

            if (!int.TryParse(details[3].Trim(), out int numberOfRaces) || numberOfRaces < 1 || numberOfRaces > 30)
            {
                Console.WriteLine("Invalid number entered. Enter a number between 1 and 30.");
                return;
            }

            // Ensure event name uniqueness
            if (FindRaceEventByName(eventName) != null)
            {
                Console.WriteLine($"Event '{eventName}' already exists.");
                return;
            }

            // Add and save the new event
            var newEvent = new RaceEvents(eventName, location, date, new List<Race>(), numberOfRaces);
            Events.Add(newEvent);
            FileUtils.WriteNewRaceEvent(newEvent);
            Console.WriteLine($"Event '{eventName}' created successfully.");
        }

        // Method to add races to an event
        public void AddRacesToEvent()
        {
            Console.WriteLine("Enter the event name to add races to:");
            string eventName = Console.ReadLine();
            var raceEvent = FindRaceEventByName(eventName);

            if (raceEvent == null)
            {
                Console.WriteLine($"Event '{eventName}' not found.");
                return;
            }

            int numberOfRaces = GetIntegerInput("Enter the number of races to add:");
            if (numberOfRaces <= 0) return;

            for (int i = 0; i < numberOfRaces; i++)
            {
                Race newRace = CreateRace();
                if (newRace != null)
                {
                    raceEvent.AddRace(newRace);
                    Console.WriteLine($"Race '{newRace.Name}' added to event '{eventName}'.");
                }
            }

            FileUtils.WriteWholeNewEventRaces(Events);
        }

        // Uploads horses to a race from a file
        public void UploadHorsesToRace()
        {
            Console.WriteLine("Enter the file path for the horse list:");
            string filePath = Console.ReadLine();
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            AddHorsesFromFile(filePath);
            FileUtils.WriteWholeNewRaces(GetAllRaces());
        }

        // Adds a single horse to a race
        public void AddSingleHorse()
        {
            Console.WriteLine("Enter the race name:");
            Race race = FindRaceByName(Console.ReadLine());

            if (race == null)
            {
                Console.WriteLine("Race not found.");
                return;
            }

            Console.WriteLine("Enter horse details in the format: Name, BirthDate (MM/dd/yyyy), ID");
            string[] details = Console.ReadLine().Split(',');
            if (details == null || details.Length != 3)
            {
                Console.WriteLine("Invalid horse details format.");
                return;
            }

            Horse horse = CreateHorse(details);
            if (horse != null && race.AddHorse(horse))
            {
                Console.WriteLine($"Horse '{horse.HorseName}' added to race '{race.Name}'.");
            }
            else
            {
                Console.WriteLine("Failed to add horse to the race.");
            }
        }

        // Helper Methods

        // Finds a race event by its name
        private RaceEvents FindRaceEventByName(string eventName)
        {
            foreach (var e in Events)
            {
                if (e.EventName.Equals(eventName, StringComparison.OrdinalIgnoreCase))
                {
                    return e;
                }
            }
            return null;
        }

        // Finds a race by its name across all events
        private Race FindRaceByName(string raceName)
        {
            foreach (var raceEvent in Events)
            {
                foreach (var r in raceEvent.Races)
                {
                    if (r.Name.Equals(raceName, StringComparison.OrdinalIgnoreCase))
                    {
                        return r;
                    }
                }
            }
            return null;
        }

        // Creates a race with user input
        private Race CreateRace()
        {
            Console.WriteLine("Enter race details in the format: Name, StartTime (hh:mm), AllowedHorses");
            string[] details = Console.ReadLine().Split(',');

            if (details == null || details.Length != 3)
            {
                Console.WriteLine("Invalid race format.");
                return null;
            }

            string raceName = details[0].Trim();
            if (!TimeSpan.TryParse(details[1].Trim(), out TimeSpan startTime))
            {
                Console.WriteLine("Invalid start time.");
                return null;
            }

            if (!int.TryParse(details[2].Trim(), out int allowedHorses) || allowedHorses < 3 || allowedHorses > 15)
            {
                Console.WriteLine("Allowed horses must be between 3 and 15.");
                return null;
            }

            return new Race(raceName, startTime, new List<Horse>(), allowedHorses);
        }

        // Adds horses from a file to races
        private void AddHorsesFromFile(string filePath)
        {
            Race currentRace = null;

            foreach (var line in File.ReadLines(filePath))
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("Race:", StringComparison.OrdinalIgnoreCase))
                {
                    currentRace = FindRaceByName(trimmedLine.Substring("Race:".Length).Trim());
                    if (currentRace == null)
                    {
                        Console.WriteLine("Race not found.");
                    }
                }
                else if (currentRace != null && !string.IsNullOrEmpty(trimmedLine))
                {
                    string[] horseData = trimmedLine.Split(',');
                    Horse horse = CreateHorse(horseData);
                    if (horse != null && currentRace.AddHorse(horse))
                    {
                        Console.WriteLine($"Horse '{horse.HorseName}' added to race '{currentRace.Name}'.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to add horse '{horse.HorseName}' to race '{currentRace.Name}'.");
                    }
                }
            }
        }

        // Creates a horse object from an array of details
        private Horse CreateHorse(string[] details)
        {
            if (details.Length != 3)
            {
                Console.WriteLine("Invalid horse data format.");
                return null;
            }

            string horseName = details[0].Trim();
            if (!DateTime.TryParseExact(details[1].Trim(), "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
            {
                Console.WriteLine("Invalid birth date format.");
                return null;
            }
            string horseId = details[2].Trim();

            return new Horse(horseName, birthDate, horseId);
        }

        // Gets integer input from the user
        private int GetIntegerInput(string prompt)
        {
            Console.WriteLine(prompt);
            int result;

            if (int.TryParse(Console.ReadLine(), out result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        // Retrieves all races across all events
        private List<Race> GetAllRaces()
        {
            var allRaces = new List<Race>();
            foreach (var raceEvent in Events)
            {
                allRaces.AddRange(raceEvent.Races);
            }
            return allRaces;
        }
    }
}
