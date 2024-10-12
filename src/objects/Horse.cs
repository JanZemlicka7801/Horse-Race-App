using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            horse = horseID;
            // this is gonna be the pattern for the ID of a horse - @"\b[A-Z]{3}\d{9}\b"
        }
    }
}
