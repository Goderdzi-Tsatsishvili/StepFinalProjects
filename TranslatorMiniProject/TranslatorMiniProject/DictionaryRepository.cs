using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorMiniProject
{
    internal class DictionaryRepository
    {
        private string fileName;

        public DictionaryRepository(string file)
        {
            fileName = file;
        }

        public Dictionary<string, string> load()
        {
            var dict = new Dictionary<string, string>();

            if (!File.Exists(fileName)) return dict;

            foreach (var line in File.ReadAllLines(fileName))
            {
                var parts = line.Split('=');

                if (parts.Length == 2) dict[parts[0]] = parts[1];
            }

            return dict;
        }

        public void Save(string word, string translation)
        {
            File.AppendAllText(fileName, $"{word}={translation}\n");
        }
    }
}
