using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanMiniGame
{
    public class GameRenderer
    {
        public void DisplayWord(string hiddenWord)
        {
            Console.WriteLine("Word: " + hiddenWord);
        }

        public void DisplayAttempts(int attempts)
        {
            Console.WriteLine("Attempts left: " +  attempts);
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
