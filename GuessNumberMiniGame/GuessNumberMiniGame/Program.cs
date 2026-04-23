namespace GuessNumberMiniGame
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Select Difficulty");
            Console.WriteLine("1. Easy (1-50, 10 attempts)");
            Console.WriteLine("2. Medium (1-100, 7 attempts)");
            Console.WriteLine("3. Hard (1-200, 5 attempts)");

            string choice = Console.ReadLine();

            
            GameSettings settings = choice switch
            {
                "1" => new GameSettings(1, 50, 10),
                "2" => new GameSettings(1, 100, 7),
                "3" => new GameSettings(1, 200, 5),
                _ => null
            };

            if(settings == null)
            {
                Console.WriteLine("Invalid Choice");
                return;
            }

            GuessGame game = new GuessGame(settings);
            game.Play(); 
        }
    }
}
