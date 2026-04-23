using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanMiniGame
{
    public class WordProvider
    {
        private string _filePath;

        public WordProvider(string filePath)
        {
            _filePath = filePath;
        }

        public string getRandomWord()
        {
            if (!File.Exists(_filePath)) throw new Exception("File Not Found");

            var words = File.ReadAllLines(_filePath)
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToList();

            if (words.Count == 0) throw new Exception("Word List is Empty");

            Random rand = new Random();
            return words[rand.Next(words.Count)];
        }
    }
}
