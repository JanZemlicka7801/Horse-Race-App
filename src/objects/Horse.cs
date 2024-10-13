using System.Text.RegularExpressions;

namespace Horse_Race_App.src.objects
{
    internal class Horse
    {
        private string horseName;
        private DateTime birthDate; //when creating (yyyy, mm, dd)
        private string horseID;

        public string HorseName
        {
            get => horseName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Horse name cannot be empty or null.");
                }
                horseName = value;
            }
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                DateTime today = DateTime.Now;
                int age = today.Year - value.Year;

                if (value > today.AddYears(-age))
                {
                    age--;
                }

                if (age < 2 || age > 5)
                {
                    throw new ArgumentException("Horse must be between 2 and 5 years old.");
                }

                if (value > today)
                {
                    throw new ArgumentException("Invalid date of birth.");
                }

                birthDate = value;
            }
        }

        public string HorseID
        {
            get => horseID;
            set
            {
                string pattern = @"^[A-Z]{3}\d{9}$";
                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Horse ID must follow the pattern: 3 uppercase letters followed by 9 digits.");
                }
                horseID = value;
            }
        }

        public Horse(string name, DateTime date, string horse) 
        {
            horseName = name;
            birthDate = date;
            horseID = horse;
        }

        public bool validateID(string id)
        {
            string pattern = @"\b[A-Z]{3}\d{9}\b";
            return Regex.IsMatch(id, pattern);
        }

        public override string ToString()
        {
            return $"{horseName} (ID: {horseID}), Born: {birthDate.ToShortDateString()}";
        }
    }
}
