using System;
using System.Globalization;
using Spectre.Console;

namespace backend_welcome_message;

class Program
{
    static void Main(string[] args)
    {
        var now = DateTime.Now;

        var greeting = BuildGreeting(now);
        AnsiConsole.WriteLine(greeting);
    }

    static string BuildGreeting(DateTime now)
    {
        var formatted = now.ToString("dddd dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        string timeOfDay;

       switch (now.Hour)
       {
        case >= 5 and <= 12:
            timeOfDay = "Good morning";
            break;
        case >= 12 and <= 18:
            timeOfDay = "Good afternoon";
            break;
        case >= 18 and <= 22:
            timeOfDay = "Good evening";
            break;
        default:
            timeOfDay = "Good night";
            break;
       }

       string dayType;
       switch (now.DayOfWeek)
       {
        case DayOfWeek.Saturday or DayOfWeek.Sunday:
            dayType = "Enjoy your weekend!";
            break;
            default:
            dayType = "Have a great day!";
            break;            
       }
       var table = new Table().Border(TableBorder.Rounded);
       table.AddColumn("[bold]Part[/]");
       table.AddColumn("[bold]Value[/]");
       table.AddRow("Greeting", $"[aqua]{timeOfDay}[/];");
       table.AddRow("Date/Time", $"[yellow]{formatted}[/]");
       table.AddRow("Day Type", $"[green]{dayType}[/]");

       return table;
    }
}
