using Horse_Race_App.objects;
using Horse_Race_App.people;
using Horse_Race_App.utils;

namespace MyNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            List<RaceEvents> pastEventsList = FileUtils.GetPastRaceEventsList();
            FileUtils.DeleteRaceEventsFromFile(pastEventsList);
            List<Race> pastRaces = FileUtils.GetAllRacesFromListOfEvents(pastEventsList);
            FileUtils.DeleteRacesFromFile(pastRaces);

            Menus.horseOwnerMenu(new HorseOwner());
        }
    }
}