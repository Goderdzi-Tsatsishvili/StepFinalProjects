using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanMiniGame
{
    public class HangmanGame
    {
        private string _word;
        private HashSet<char> _guessedLetter;
        private int _attempted;

        public HangmanGame(string word, int maxAttempts = 6)
        {
            _word = word;
            _guessedLetter = new HashSet<char>();
            _attempted = maxAttempts;
        }

        public int attemptsLeft => _attempted;

        public bool guess(char letter)
        {
            letter = char.ToLower(letter);

            if (_guessedLetter.Contains(letter)) return false;

            _guessedLetter.Add(letter);

            if (!_word.Contains(letter)) _attempted--;

            return true;
        }

        public string hiddenWord()
        {
            string result = "";

            foreach(char c in _word)
            {
                if (_guessedLetter.Contains(c))
                {
                    result += c + " ";
                }
                else
                {
                    result += "_ ";
                }
            }

            return result.Trim();
        }

        public bool isWon()
        {
            return _word.All(c => _guessedLetter.Contains(c));
        }

        public bool isGameLost()
        {
            return _attempted <= 0 || isWon();
        }

        public string getWord()
        {
            return _word;
        }
    }
}
