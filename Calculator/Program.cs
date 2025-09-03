
using System.Text.RegularExpressions;
// CalculatorLibrary.cs
using System.Diagnostics;
using System.Globalization;
using CalculatorLibrary;

namespace CalculatorProgram
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> calculatorList = new List<string>();
            Calculator calculator = new Calculator();
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                ShowMenu();

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                string completeCalulator = $"{cleanNum1} {Transfer(op)} {cleanNum2} = {result}";

                calculatorList.Add(completeCalulator);
                int totalUsed = calculatorList.Count();
                Console.WriteLine($"You have used calculator: {totalUsed} time");
                Console.WriteLine("------------------------\n");
                // Wait for the user to respond before closing.
              
                Console.Write("Press 'n' and Enter to close the app, Press 'l' to show latest calculation, or press any other key and Enter to continue: ");
                string? userInput = Console.ReadLine();
                if (userInput == "n")
                {
                    endApp = true;
                }
                while (userInput == "l")
                {
                    string showLatestCalculation = calculatorList[calculatorList.Count() -1 ].ToString();
                    Console.WriteLine(showLatestCalculation);

                    Console.WriteLine("Do you want to use this result again to calculate y/n");
                    userInput = Console.ReadLine();
                    while (userInput == "y")
                    {
                        cleanNum1 = result;
                        Console.WriteLine($"{cleanNum1}");

                        Console.WriteLine("Enter your operation a, s, m, d");
                        op = Console.ReadLine();

                        Console.WriteLine($"Your number: {cleanNum1} {Transfer(op)} ??");
                        numInput2 = Console.ReadLine();
                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        Console.WriteLine("Result: " + result);
                        Console.WriteLine("Do you want to use this result again to calculate y/n");
                        userInput = Console.ReadLine();
                    }
                }
                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
        public static void ShowMenu()
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\te - Exit");
            Console.WriteLine("Your option? ");
        }
        public static string Transfer(string original)
        {
            string translateOriginal = original switch
            {
                "a" => "+",
                "s" => "-",
                "m" => "*",
                "d" => "/",
                _ => throw new Exception("Invalid operation")
            };
            return translateOriginal;
        }
    }
}
