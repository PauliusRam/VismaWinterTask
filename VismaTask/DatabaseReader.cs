using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VismaTask
{
    class DatabaseReader
    {
        const string databasePath = "dadjokedatabase.txt";
        public List<JokeTemplate> LoadJokes()                                                             // Preloads jokes to list
        {
            List<JokeTemplate> jokeList = new List<JokeTemplate>();
            string[] lines = File.ReadAllLines(@databasePath);
            if (lines.Length > 1)
            {
                for (int i = 0; i < lines.Length; i = i + 2)
                {
                    jokeList.Add(new JokeTemplate(jokeList.Count + 1, lines[i], lines[i + 1]));
                }
            }
            return jokeList;
        }
        public void InsertToFile(string question,string answer)
        {
            using (StreamWriter file = new StreamWriter(@databasePath, true))
            {
                file.WriteLine("\n"+question);
                file.Write(answer);
                file.Close();
            }
        }
    }
}
