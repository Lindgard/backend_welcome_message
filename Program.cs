namespace backend_welcome_message;

using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        var now = DateTime.Now;

        var greeting = BuildGreeting(now);
        Console.WriteLine(greeting);
    }
}
