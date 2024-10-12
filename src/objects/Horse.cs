using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Horse_Race_App.src.objects
{
    internal class Horse
    {
        public string horseName { get; set; }
        public DateTime birthDate { get; set; } //when creating (yyyy, mm, dd)
        public string horseID { get; set; }

        public Horse(string name, DateTime date, string horse) 
        {
            horseName = name;
            birthDate = date;
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
