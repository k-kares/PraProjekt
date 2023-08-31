using System.Linq.Expressions;

namespace Utilities
{
    public class FileUtilities
    {
        //kolegiji se save-aju name|usersName
        //obavijesti se save-aju ime kolegija|title|message
        private const char SEPARATOR = '|';

        public static List<string> LoadFileData(string PATH)
        {
            List<string> fileData = new List<string>();
            if (!File.Exists(PATH))
            {
                return fileData;
            }

            string[] lines = File.ReadAllLines(PATH);

            foreach (string line in lines)
            {
                string[] details = line.Split(SEPARATOR);
                for (int i = 0; i < details.Length; i++)
                {
                    fileData.Add(details[i]);
                }
            }

            return fileData;
        }

        //Nije testirano
        public static List<Obavijest> LoadFileDataforObavijest(string PATH)
        {
            List<Obavijest> fileData = new List<Obavijest>();
            if (!File.Exists(PATH))
            {
                return fileData;
            }

            string[] lines = File.ReadAllLines(PATH);

            foreach (string line in lines)
            {
                string[] details = line.Split(SEPARATOR);

                Obavijest ob = new Obavijest();
                ob.ImeKolegija = details[0];
                ob.Title = details[1];
                ob.Message = details[2];

                fileData.Add(ob);
            }

            return fileData;
        }

        //Nije testirano
        public static List<Kolegij> LoadFileDataforKolegiji(string PATH)
        {
            List<Kolegij> fileData = new List<Kolegij>();
            if (!File.Exists(PATH))
            {
                return fileData;
            }

            string[] lines = File.ReadAllLines(PATH);

            foreach (string line in lines)
            {
                string[] details = line.Split(SEPARATOR);

                Kolegij k = new Kolegij();
                k.Name = details[0];
                k.UsersName = details[1];

                fileData.Add(k);
            }

            return fileData;
        }
    }
}