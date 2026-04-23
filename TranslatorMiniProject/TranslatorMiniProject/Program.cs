namespace TranslatorMiniProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select language pair:");
            Console.WriteLine("1. Georgian -> English");
            Console.WriteLine("2. English -> Georgian");

            string choice = Console.ReadLine();

            string file = choice == "1" ? "../../../geo-eng.txt" : "../../../eng-geo.txt";

            var repo = new DictionaryRepository(file);
            var dict = repo.load();

            LanguageTranslator translator = choice == "1" ? new GeoEngTranslator(dict) : new EngGeoTranslator(dict);

            Console.Write("Enter word: ");
            string input = Console.ReadLine();

            string result = translator.Translate(input);

            if (result != null)
            {
                Console.WriteLine("Translation: " + result);
            }
            else
            {
                Console.WriteLine("Word not found.");
                Console.Write("Do you want to add it? (yes/no): ");
                string answer = Console.ReadLine();

                if (answer.ToLower() == "yes")
                {
                    Console.Write("Enter translation: ");
                    string translation = Console.ReadLine();

                    repo.Save(input, translation);

                    Console.WriteLine("Word added successfully!");
                }
            }
        }
    }
}
