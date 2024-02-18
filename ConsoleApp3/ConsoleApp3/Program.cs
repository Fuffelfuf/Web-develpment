using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void ThreadFunc()
    {
            Console.WriteLine("Thread Func result");
    }

    static void TaskFunc()
    {
            Console.WriteLine("Task Func result");
    }

    static async Task Main(string[] args)
    {
        Thread thread1 = new Thread(new ThreadStart(ThreadFunc));
        Task task1 = Task.Factory.StartNew(() => TaskFunc());
        
        Console.WriteLine(await ATask());
        Console.WriteLine(await AsyncFunc());
        await Task.WhenAny(task1);
        thread1.Start();
        Console.WriteLine("Main End");
        Console.ReadLine();
    }

    static async Task<string> AsyncFunc()
    {
        string result = await ATask();
        return result+" from AsyncFunc";
    }

    static async Task<string> ATask()
    {
        return "ATask result";
    }
}