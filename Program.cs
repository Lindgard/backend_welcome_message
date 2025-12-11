using System;
using System.Globalization;
using Spectre.Console;

namespace backend_welcome_message;

class Program
{
    static void Main(string[] args)
    {
        var now = DateTime.Now;
        
        var name = AnsiConsole.Ask<string>("What is your [green]name[/]?");
        if (string.IsNullOrWhiteSpace(name))
            name = "Guest";

        var table = BuildGreeting(now, name);
        AnsiConsole.Write(table);
    }

    static Table BuildGreeting(DateTime now, string name)
    {
        var formatted = now.ToString("dddd dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
        string timeOfDay;

        switch (now.Hour)
        {
            case >= 5 and < 12:
                timeOfDay = "Good morning";
                break;
            case >= 12 and < 18:
                timeOfDay = "Good afternoon";
                break;
            case >= 18 and < 22:
                timeOfDay = "Good evening";
                break;
            default:
                timeOfDay = "Good night";
                break;
        }

        string dayType;
        switch (now.DayOfWeek)
        {
            case DayOfWeek.Saturday:
            case DayOfWeek.Sunday:
                dayType = $"Enjoy your {now.ToString("dddd", CultureInfo.InvariantCulture)}! Enjoy your weekend!";
                break;
            default:
                dayType = $"Have a great {now.ToString("dddd", CultureInfo.InvariantCulture)}!";
                break;
        }

        var table = new Table().Border(TableBorder.Rounded);
        table.AddColumn("[bold]Part[/]");
        table.AddColumn("[bold]Value[/]");
        table.AddRow($"[aqua]Greeting[/]", $"[aqua]{timeOfDay}, {name}![/]");
        table.AddRow($"[yellow]Date/Time[/]", $"[yellow]{formatted}[/]");
        table.AddRow($"[green]Day Type[/]", $"[green]{dayType}[/]");

        return table;
    }
}
