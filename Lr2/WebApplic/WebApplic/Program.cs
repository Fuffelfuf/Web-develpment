using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Serilog;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
class Program
{
    
    private static readonly HttpClient client = new HttpClient();
    // 2.2 System.Net.Http, 2.3 System.Threading
    public static async Task<string> GetDataAsync(string url)
    {
        var response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
         return null;
    }
    static async Task Main(string[] args)
    {

        // 1.1 System.IO
        string filePath = "files\\test.txt";
        File.WriteAllText(filePath, "Hello, World!");
        string fileContent = File.ReadAllText(filePath);
        Console.WriteLine($"File content: {fileContent}");
        Console.WriteLine("-----------");

        // 1.2 Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        Log.Information("Serilog message");
        Console.WriteLine("-----------");

        // 1.3 System.Diagnostics
        Console.WriteLine("Start calculator");
        Process.Start("calc.exe");
        Console.WriteLine("-----------");

        // 1.4 System.Collections.Generic
        Dictionary<string, int> people = new Dictionary<string, int>();
        people.Add("John", 30);
        people.Add("Alice", 25);
        people.Add("Bob", 35);
        Console.WriteLine($"Alice age: {people["Alice"]}");
        Console.WriteLine("-----------");

        // 1.5 Newtonsoft.Json
        var person = new { Name = "John", Age = 30 };
        string json = JsonConvert.SerializeObject(person);
        System.Console.WriteLine(json);
        Console.WriteLine("-----------");
        var deserializedPerson = JsonConvert.DeserializeObject(json);
        System.Console.WriteLine(deserializedPerson);
        Console.WriteLine("-----------");

        Console.WriteLine("===================");
        // 2.1 System.Data.SQLite
        var connection = new SQLiteConnection("Data Source=TestSQLite.db;Version=3");
        connection.Open();
        var name = new SQLiteCommand("SELECT Name FROM Users LIMIT 1;", connection).ExecuteScalar()?.ToString();
        Console.WriteLine(name);
        connection.Close();
        Console.WriteLine("------------------");


        // 2.2 System.Net.Http
        string data = await GetDataAsync("https://api.coincap.io/v2/assets");
        if (data != null)
        {
            Console.WriteLine("Полученные данные:");
            int maxIndex = Math.Min(80, data.Length);
            for (int i = 0; i < maxIndex; i++)
            {
                Console.Write(data[i]);
            }
        }
        else
        {
            Console.WriteLine("Не удалось получить данные.");
        }
        Console.WriteLine("\n-----------");
        // 2.4 System.Linq
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };        
        var evenNumbers = numbers.Where(n => n % 2 == 0);
        Console.WriteLine("Четные числа:");
        foreach (var number in evenNumbers)
        {
            Console.WriteLine(number);
        }
        Console.WriteLine("-------------");
        // 2.5 System.Reflection
        Type type = typeof(int);
        TypeInfo typeInfo = type.GetTypeInfo();
        Console.WriteLine(typeInfo);
        Console.ReadLine();
     }
}