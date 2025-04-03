using System;
using System.Linq;
using Dab.Days;

namespace PublicHolidaysCalculator.ConsoleTester;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            //TestIfDateIsDayOff();
            //TestNbWorkingDaysForMonthAndYear();
            //TestHolidaysForYear();

            var today = DateTime.Today; // new DateTime(2024, 1, 1);
            Console.WriteLine($"Nous sommes le {today}.");
            Console.WriteLine();

            var frenchPublicHolidaysCalculator = new FrenchPublicHolidaysCalculator();
            var workingDaysCalculator = new WorkingDaysCalculator(frenchPublicHolidaysCalculator);

            var isDayOff = workingDaysCalculator.IsDayOff(today);
            var dayOffLabel = isDayOff ? "non " : "";
            Console.WriteLine($"Le {today.ToLongDateString()} est un jour {dayOffLabel}travaillé en France.");
            Console.WriteLine();

            var nbWorkingDays = workingDaysCalculator.GetWorkingDaysByMonth(today.Year, today.Month);
            Console.WriteLine($"Il y a {nbWorkingDays} jour(s) travaillé(s) en {today.ToString("MMMM")} {today.Year}.");
            Console.WriteLine();

            var result = frenchPublicHolidaysCalculator.GetPublicHolidays(today.Year);
            Console.WriteLine($"Il y a {result.Count()} jours fériés en France en {today.Year} :");
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString("D"));
            }
            Console.WriteLine();

            var startDate = today; //new DateTime(today.Year, 1, 1);
            var endDate = today.AddYears(1); //new DateTime(today.Year, 12, 31);
            var nbDays = workingDaysCalculator.GetWorkingDaysBetweenDates(startDate, endDate);
            Console.WriteLine($"Il y a {nbDays} jours travaillés en France entre le {startDate.ToShortDateString()} et le {endDate.ToShortDateString()}.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void TestIfDateIsDayOff()
    {
        while (true)
        {
            Console.WriteLine("Saisir une date pour déterminer si c'est un jour férié ou non travaillé (ex: 01/01/2020):");
            var wdate = Console.ReadLine();
            var res = DateTime.TryParse(wdate, out DateTime date);
            if (!res)
            {
                Console.WriteLine("Date incorrecte.");
                continue;
            }

            var isDayOff = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator()).IsDayOff(date);
            var dayOffLabel = isDayOff ? "non " : "";

            Console.WriteLine($"{date} est un jour {dayOffLabel}travaillé.");
        }
    }

    private static void TestNbWorkingDaysForMonthAndYear()
    {
        while (true)
        {
            Console.WriteLine($"Saisir une année pour connaître ses jours travaillés (ex: 2020):");
            var enteredYear = Console.ReadLine();
            var res = int.TryParse(enteredYear, out var year);
            if (!res)
            {
                Console.WriteLine("Année incorrecte.");
                continue;
            }
            Console.WriteLine("Saisir un numéro de mois (ex: 1 à 12):");
            var enteredMonth = Console.ReadLine();
            res = int.TryParse(enteredMonth, out var month);
            if (!res)
            {
                Console.WriteLine("Mois incorrect.");
                continue;
            }
            var result = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator()).GetWorkingDaysByMonth(year, month);

            Console.WriteLine($"{result} jour(s) travaillé(s) en {month}/{year}.");
        }
    }

    private static void TestHolidaysForYear()
    {
        while (true)
        {
            Console.WriteLine($"Saisir une année pour connaître ses jours fériés (ex: 2020):");
            var enteredYear = Console.ReadLine();
            var res = int.TryParse(enteredYear, out var year);
            if (!res)
            {
                Console.WriteLine("Année incorrecte.");
                continue;
            }
            var result = new FrenchPublicHolidaysCalculator().GetPublicHolidays(year);
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString("D"));
            }
        }
    }
}
