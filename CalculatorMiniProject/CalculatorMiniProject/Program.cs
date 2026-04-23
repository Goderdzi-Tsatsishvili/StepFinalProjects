namespace CalculatorMiniProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the first number: ");
            if(!double.TryParse(Console.ReadLine(), out double a))
            {
                Console.WriteLine("Invalid Input");
                return;
            }

            Console.Write("Enter the second number: ");
            if (!double.TryParse(Console.ReadLine(), out double b))
            {
                Console.WriteLine("Invalid Input");
                return;
            }

            Console.Write("Enter operator (+, -, *, /): ");

            string choice = Console.ReadLine();

            OperationManager operation = OperationFactory.Create(choice, a, b);

            if (operation == null)
            {
                Console.WriteLine("Invalid Operation");
                return;
            }

            try
            {
                Console.WriteLine("Result: " + operation.Calculate());
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
