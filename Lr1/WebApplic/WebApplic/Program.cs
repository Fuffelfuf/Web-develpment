using System;
using System.IO;
using System.Linq;


class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Output Words from Text");
            Console.WriteLine("2. Perform Math Operation");
            Console.WriteLine("0. Exit");
            Console.Write("Enter the option number: ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }
            switch (choice)
            {
                case 1:
                    OutputWords();
                    break;
                case 2:
                    PerformMathOperation();
                    break;
                case 0:
                    Console.WriteLine("Thank you for using the program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void OutputWords()
    {
            Console.Write("Enter the number of words: ");

            if (!int.TryParse(Console.ReadLine(), out int wordsCount) || wordsCount <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
                return;
            }

            string filePath = "files\\LoremIpsum.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File '{filePath}' not found. Make sure the file is in the correct directory.");
                return;
            }

            string text = File.ReadAllText(filePath);
            string[] words = text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Join(" ", words.Take(wordsCount));
            Console.WriteLine($"First {wordsCount} words from the text:");
            Console.WriteLine(result);
    }
    static void PerformMathOperation()
    {
        double num1, num2;

        Console.Write("Enter the first argument: ");
        if (!double.TryParse(Console.ReadLine(), out num1))
        {
            Console.WriteLine("Invalid input for the first argument.");
            return;
        }

        Console.Write("Enter the second argument: ");
        if (!double.TryParse(Console.ReadLine(), out num2))
        {
            Console.WriteLine("Invalid input for the second argument.");
            return;
        }

        Console.WriteLine("Choose operation:");
        Console.WriteLine("1. Addition (+)");
        Console.WriteLine("2. Subtraction (-)");
        Console.WriteLine("3. Multiplication (*)");
        Console.WriteLine("4. Division (/)");

        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        switch (choice)
        {
            case 1:
                Console.WriteLine($"Result of addition: {num1 + num2}");
                break;
            case 2:
                Console.WriteLine($"Result of subtraction: {num1 - num2}");
                break;
            case 3:
                Console.WriteLine($"Result of multiplication: {num1 * num2}");
                break;
            case 4:
                if (num2 == 0)
                {
                    Console.WriteLine("Error: Division by zero.");
                }
                else
                {
                    Console.WriteLine($"Result of division: {num1 / num2}");
                }
                break;
            default:
                Console.WriteLine("Invalid operation choice.");
                break;
        }
    }
}