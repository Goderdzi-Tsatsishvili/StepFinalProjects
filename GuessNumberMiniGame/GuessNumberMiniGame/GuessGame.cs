using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GuessNumberMiniGame
{
    public class GuessGame
    {
        private int _targetNumber;
        private int _attemptsLeft;
        private GameSettings _settings;

        public GuessGame(GameSettings settings)
        {
            _settings = settings;
            _attemptsLeft = settings.maxAttempts;

            Random rand = new Random();
            _targetNumber = rand.Next(settings.minNumber, settings.maxNumber + 1);
        }

        public void Play()
        {
            while(_attemptsLeft > 0)
            {
               
                Console.WriteLine($"\nGuess a number between {_settings.minNumber} and {_settings.maxNumber}");
                Console.WriteLine($"Attempts left: {_attemptsLeft}");

                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();

                
                if(!int.TryParse(input, out int guess))
                {
                    Console.WriteLine("Invalid input, enter a number");
                    continue;
                }

                
                CheckGuess(guess);

                if(guess == _targetNumber)
                {
                    Console.WriteLine("You win");
                    return;
                }

                _attemptsLeft--;
            }

            Console.WriteLine($"you lost, the number was {_targetNumber}");
        }

        
        private void CheckGuess(int guess)
        {
            
            if (guess > _targetNumber) Console.WriteLine("too high");

            
            else if (guess < _targetNumber) Console.WriteLine("too low");
        }
    }
}
