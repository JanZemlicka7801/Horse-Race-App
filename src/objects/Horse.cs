using System.Text.RegularExpressions;

namespace Horse_Race_App.objects
{
    public class Horse
    {
        private string _horseName;
        private DateTime _birthDate; //when creating (yyyy, mm, dd)
        private string _horseId;

        public string HorseName
        {
            get => _horseName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Horse name cannot be empty or null.");
                }
                _horseName = value;
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
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

                _birthDate = value;
            }
        }

        public string HorseId
        {
            get => _horseId;
            set
            {
                string pattern = @"^[A-Z]{3}\d{9}$";
                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Horse ID must follow the pattern: 3 uppercase letters followed by 9 digits.");
                }
                _horseId = value;
            }
        }

        public Horse(string name, DateTime date, string horse) 
        {
            _horseName = name;
            _birthDate = date;
            _horseId = horse;
        }

        public bool ValidateId(string id)
        {
            string pattern = @"\b[A-Z]{3}\d{9}\b";
            return Regex.IsMatch(id, pattern);
        }

        public override string ToString()
        {
            return $"{_horseName} (ID: {_horseId}), Born: {_birthDate.ToShortDateString()}";
        }
    }
}
