using System.Linq.Expressions;

namespace Utilities
{
    public class FileUtilities
    {
        //kolegiji se save-aju name|usersName|ID
        //obavijesti se save-aju ime kolegija|title|message|ID
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
                if(string.IsNullOrEmpty(line)) 
                    continue;

                string[] details = line.Split(SEPARATOR);

                Obavijest ob = new Obavijest();
                ob.ImeKolegija = details[0];
                ob.Title = details[1];
                ob.Message = details[2];
                ob.ImePredavaca = details[3];
                ob.DatumObjave = details[4];
                ob.DatumIsteka = details[5];
                ob.ID = details[6];

                fileData.Add(ob);
            }

            return fileData;
        }

        public static List<Obavijest> LoadFileDataforObavijest(string PATH, Obavijest ovaObavijest)
        {
            List<Obavijest> fileData = new List<Obavijest>();
            if (!File.Exists(PATH))
            {
                return fileData;
            }

            string[] lines = File.ReadAllLines(PATH);

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] details = line.Split(SEPARATOR);

                Obavijest ob = new Obavijest();
                ob.ImeKolegija = details[0];
                ob.Title = details[1];
                ob.Message = details[2];
                ob.ImePredavaca = details[3];
                ob.DatumObjave = details[4];
                ob.DatumIsteka = details[5];
                ob.ID = details[6];

                fileData.Add(ob);
            }

            return fileData;
        }

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
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] details = line.Split(SEPARATOR);

                Kolegij k = new Kolegij();
                k.Name = details[0];
                k.UsersName = details[1];
                k.ID = details[2];

                fileData.Add(k);
            }

            return fileData;
        }
        public static List<User> LoadFileDataforKorisnici(string PATH)
        {
            List<User> fileData = new List<User>();
            if (!File.Exists(PATH))
            {
                return fileData;
            }

            string[] lines = File.ReadAllLines(PATH);

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                string[] details = line.Split(SEPARATOR);

                User u = new User();
                u.Name = details[0];
                u.Email = details[1];
                u.Password = details[2];
                u.IsAdmin = bool.Parse(details[3]);
                u.ID = details[4];

                fileData.Add(u);
            }

            return fileData;
        }
    }
}