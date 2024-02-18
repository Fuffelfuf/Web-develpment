using System;
using System.IO;

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
        try
        {
            Console.Write("Enter the number of words: ");

            int wordsCount;
            if (!int.TryParse(Console.ReadLine(), out wordsCount) || wordsCount <= 0)
            {
                Console.WriteLine("Invalid input.");
                return;
            }
            string text = File.ReadAllText("C:\\Users\\Viktor\\source\\repos\\ConsoleApp1\\ConsoleApp1\\files\\LoremIpsum.txt");
            string[] words = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine($"First {wordsCount} words from the text:");
            for (int i = 0; i < Math.Min(wordsCount, words.Length); i++)
            {
                Console.WriteLine(words[i]);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File 'LoremIpsum.txt' not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void PerformMathOperation()
    {
        Console.Write("Enter the first argument: ");
        string arg1 = Console.ReadLine();
        Console.Write("Enter the second argument: ");
        string arg2 = Console.ReadLine();
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
                Console.WriteLine($"Result of addition: {Convert.ToDouble(arg1) + Convert.ToDouble(arg2)}");
                break;
            case 2:
                Console.WriteLine($"Result of subtraction: {Convert.ToDouble(arg1) - Convert.ToDouble(arg2)}");
                break;
            case 3:
                Console.WriteLine($"Result of multiplication: {Convert.ToDouble(arg1) * Convert.ToDouble(arg2)}");
                break;
            case 4:
                if (Convert.ToDouble(arg2) == 0)
                {
                    Console.WriteLine("Error: Division by zero.");
                }
                else
                {
                    Console.WriteLine($"Result of division: {Convert.ToDouble(arg1) / Convert.ToDouble(arg2)}");
                }
                break;
            default:
                Console.WriteLine("Invalid operation choice.");
                break;
        }
    }
}