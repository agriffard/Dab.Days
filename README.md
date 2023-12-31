# Dab.Days

Calcul des jours fériés et des jours ouvrés par mois et année en France.

[![Nuget](https://img.shields.io/nuget/dt/Dab.Days)](https://www.nuget.org/packages/Dab.Days)

## Examples

https://dotnetfiddle.net/ql8sZW


``` csharp
 var today = DateTime.Today;
 Console.WriteLine($"Nous sommes le {today}.");

 var isDayOff = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator()).IsDayOff(today);
 var dayOffLabel = isDayOff ? "non " : "";
 Console.WriteLine($"Le {today.ToLongDateString()} est un jour {dayOffLabel}travaillé en France.");

 var nbWorkingDays = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator()).GetWorkingDaysByMonth(today.Year, today.Month);
 Console.WriteLine($"Il y a {nbWorkingDays} jour(s) travaillé(s) en {today.ToString("MMMM")} {today.Year}.");

 var result = new FrenchPublicHolidaysCalculator().GetPublicHolidays(today.Year);
 Console.WriteLine($"Il y a {result.Count()} jours fériés en France en {today.Year} :");
 foreach (var item in result)
 {
     Console.WriteLine(item.ToString("D"));
 }
```

## Credits

https://github.com/anna-git/PublicHolidaysCalculator
