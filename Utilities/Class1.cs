namespace Utilities
{
    public class FileUtilities
    {
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
    }
}