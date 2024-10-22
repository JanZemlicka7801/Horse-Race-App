using System.Text.RegularExpressions;

namespace Horse_Race_App.objects
{
    public class Horse
    {
        public string HorseName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HorseId { get; set; }

        public Horse(string name, DateTime birthDate, string horseId)
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

            if (!ValidateHorseId(horseId))
            {
                Console.WriteLine("Invalid Horse ID. It must follow the pattern: 3 uppercase letters followed by 9 digits.");
                return;
            }

            HorseName = name;
            BirthDate = birthDate;
            HorseId = horseId;
        }

        // validating the name of the horse
        private bool ValidateHorseName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        // validating the age of the horse
        private bool ValidateHorseAge(DateTime birthDate)
        {
            //get the current date
            DateTime today = DateTime.Now;
            //substract the current year with current year
            int age = today.Year - birthDate.Year;
            //if the birthday is later this year then -1 year
            if (birthDate > today.AddYears(-age)) age--; 
            //validating the return
            return age is >= 2 and <= 5;
        }

        // validating ID
        private bool ValidateHorseId(string horseId)
        {
            string pattern = @"^[A-Z]{3}\d{9}$";
            return Regex.IsMatch(horseId, pattern);
        }

        public override string ToString()
        {
            return $"{HorseName} (ID: {HorseId}), Born: {BirthDate.ToShortDateString()}";
        }
    }
}