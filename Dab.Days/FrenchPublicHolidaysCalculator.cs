namespace Dab.Days;

public class FrenchPublicHolidaysCalculator : IPublicHolidaysCalculator
{
    static readonly MonthDay[] s_fixedPublicHolidays = {
        new MonthDay(01, 01), // 1er Janvier
        new MonthDay(05, 01), // 1er Mai
        new MonthDay(05, 08), // 8 Mai
        new MonthDay(07, 14), // 14 Juillet
        new MonthDay(08, 15), // 15 Août
        new MonthDay(11, 1), // 1er Novembre
        new MonthDay(11, 11), // 11 Novembre
        new MonthDay(12, 25) }; // 25 Décembre

    public IEnumerable<DateTime> GetPublicHolidays(int year)
    {
        var allDates = s_fixedPublicHolidays.Select(e => e.ToDateTime(year)).ToList();
        var easterSunday = EasterSunday(year).ToDateTime(year);
        var easterMonday = easterSunday.AddDays(1);
        var ascensionThursday = easterSunday.AddDays(39);
        var pentecoteMonday = easterSunday.AddDays(50);
        allDates.Add(easterMonday); // Lundi de Pâques
        allDates.Add(ascensionThursday); // Jeudi de l'Ascension 
        //allDates.Add(pentecoteMonday); // Lundi de Pentecôte

        return allDates.OrderBy(d => d);
    }

    private static MonthDay EasterSunday(int year)
    {
        var g = year % 19;
        var c = year / 100;
        var h = (c - (c / 4) - (((8 * c) + 13) / 25) + (19 * g) + 15) % 30;
        var i = h - (h / 28 * (1 - (h / 28 * (29 / (h + 1)) * ((21 - g) / 11))));
        var day = i - ((year + (year / 4) + i + 2 - c + (c / 4)) % 7) + 28;
        var month = 3;

        if (day > 31)
        {
            month++;
            day -= 31;
        }
        return new MonthDay(month, day);
    }
}
