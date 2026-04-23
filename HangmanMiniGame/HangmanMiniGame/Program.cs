using System.Xml;

namespace HangmanMiniGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../Words.txt";

            var wordProvider = new WordProvider(filePath);
            var renderer = new GameRenderer();

            string word;

            try
            {
                word = wordProvider.getRandomWord();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            var game = new HangmanGame(word);

            while (!game.isGameLost())
            {
                Console.Clear();
                renderer.DisplayWord(game.hiddenWord());
                renderer.DisplayAttempts(game.attemptsLeft);

                Console.Write("Enter Letter: ");
                string input = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(input) || input.Length != 1)
                {
                    renderer.DisplayMessage("Invalid input");
                    continue;
                }

                char letter = input[0];
                game.guess(letter);
            }

            Console.Clear();

            if (game.isWon())
            {
                renderer.DisplayMessage("Game Won");
            }
            else
            {
                renderer.DisplayMessage("You Lost");
                renderer.DisplayMessage("The word was: " + game.getWord());
            }
        }
    }
}
