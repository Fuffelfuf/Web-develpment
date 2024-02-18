using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        
        // 1. System.IO
        string filePath = "test.txt";
        File.WriteAllText(filePath, "Hello, World!");
        string fileContent = File.ReadAllText(filePath);
        Console.WriteLine($"File content: {fileContent}");
        Console.WriteLine("-----------");

        // 2. System.Text.RegularExpressions
        string input = "lamp boul wizard lizard cup foot hand cat dog head";
        string pattern = @"\b\w{4}\b";// words with 4 letters
        MatchCollection matches = Regex.Matches(input, pattern);
        Console.WriteLine("Words with 4 letters:");
        foreach (Match match in matches)
        {
            Console.WriteLine(match.Value);
        }
        Console.WriteLine("-----------");

        // 3. System.Diagnostics
        Console.WriteLine("Start calculator");
        Process.Start("calc.exe");
        Console.WriteLine("-----------");

        // 4. System.Collections.Generic
        Dictionary<string, int> ages = new Dictionary<string, int>();
        ages.Add("John", 30);
        ages.Add("Alice", 25);
        ages.Add("Bob", 35);
        Console.WriteLine($"Alice age: {ages["Alice"]}");
        Console.WriteLine("-----------");

        // 5. System.Threading
        Thread thread = new Thread(new ThreadStart(TestTask));
        thread.Start();
        Console.WriteLine("Main thread finished.");
        Console.ReadLine();

    }
    static void TestTask()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Test Task complete");
    }
}