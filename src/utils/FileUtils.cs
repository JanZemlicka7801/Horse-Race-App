using Horse_Race_App.objects;

namespace Horse_Race_App.utils
{
    public static class FileUtils
    {
        private const string HorseFilePath =
            "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\Horse.txt";

        private const string RaceFilePath =
            "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\Race.txt";

        private const string RaceEventFilePath =
            "C:\\Users\\lomze\\OneDrive - Dundalk Institute of Technology\\DkIT\\Web Frameworks\\Lab work\\Horse Race App\\src\\utils\\RaceEvent.txt";

        private static List<(string HorseId, DateTime BirthDate, string HorseName)> ReadHorseData()
        {
            var rawHorseData = new List<(string, DateTime, string)>();
            foreach (var line in File.ReadLines(HorseFilePath))
            {
                var parts = line.Split(',');
                string horseId = parts[0].Split(':')[1];
                DateTime birthDate = DateTime.Parse(parts[1].Split(':')[1]);
                string horseName = parts[2].Split(':')[1];
                rawHorseData.Add((horseId, birthDate, horseName));
            }

            return rawHorseData;
        }
        
        public static List<Horse> DataToHorses()
        {
            var horses = new List<Horse>();
            var rawHorseData = ReadHorseData();
            foreach (var (horseId, birthDate, horseName) in rawHorseData)
            {
                var horse = new Horse(horseName, birthDate, horseId, skipValidation: true);
                horses.Add(horse);
            }
            return horses;
        }
    }
}
