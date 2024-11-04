using System.Text.RegularExpressions;
using Horse_Race_App.utils;

namespace Horse_Race_App.objects
{
    public class Horse
    {
        private static List<Horse> listOfSavedHorses;
        
        public string HorseName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HorseId { get; set; }

        /**
         * Main constructor for a horse, its name, date of birth and id. If the new horse is being created it is set to false
         * by default, when reading from a file where the data has been validated already the boolean will be set to true.
         */
        public Horse(string name, DateTime birthDate, string horseId, bool skipValidation = false)
        {
            if (!skipValidation)
            {
                if (!ValidateHorseName(name))
                {
                    Console.WriteLine("Invalid horse name. It cannot be empty or null.");
                    return;
                }

                if (!ValidateHorseAge(birthDate))
                {
                    Console.WriteLine("Invalid horse birth date. Age must be between 2 and 5 years.");
                    return;
                }

                if (!ValidateHorseId(horseId, GetSavedHorses()))
                {
                    Console.WriteLine("Invalid Horse ID. It must follow the pattern: 3 uppercase letters followed by 9 digits.");
                    return;
                }
            }

            HorseName = name;
            BirthDate = birthDate;
            HorseId = horseId;
        }

        /**
         * Retrieves a list of horses data from file with stored horses.
         * (* won't retrieve a horses as objects because it is not necessary only working with those data is necessary)
         */
        private static List<Horse> GetSavedHorses()
        {
            if (listOfSavedHorses == null)
            {
                listOfSavedHorses = FileUtils.DataToHorses();
            }
            return listOfSavedHorses;
        }

        /**
         * Validates name of a horse, won't accept empty or null.
         */
        public static bool ValidateHorseName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        /**
         * Validates horse age that needs to be between 2 and 5 years old.
         */
        public static bool ValidateHorseAge(DateTime birthDate)
        {
            //get the current date
            DateTime today = DateTime.Now;
            //subtract the current year with current year
            int age = today.Year - birthDate.Year;
            //if the birthday is later this year then -1 year
            if (birthDate > today.AddYears(-age)) age--; 
            //validating the return
            return age is >= 2 and <= 5;
        }

        /**
         * Validates an ID of horse if it is not already in the system using a pattern.
         */
        public static bool ValidateHorseId(string horseId, List<Horse> savedHorses)
        {
            foreach (var horse in savedHorses)
            {
                if (horse.HorseId.Equals(horseId))
                {
                    Console.WriteLine("Your horse is already saved.");
                    return false;
                }
            }
            string pattern = @"^[A-Z]{3}\d{9}$";
            return Regex.IsMatch(horseId, pattern);
        }

        /**
         * Displays data in a nice formatted text.
         */
        public override string ToString()
        {
            return $"{HorseName} (ID: {HorseId}), Born: {BirthDate.ToShortDateString()}";
        }
    }
}