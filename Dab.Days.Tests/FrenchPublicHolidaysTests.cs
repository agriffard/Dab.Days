namespace Dab.Days.Tests;

public class FrenchPublicHolidaysTests
{
    public static IEnumerable<DateTime> GetPholidaysDatas(int year)
    {
        return year switch
        {
            // Pâques, Ascension
            2023 => new List<DateTime> { new DateTime(year, 4, 10), new DateTime(year, 5, 18) },
            2024 => new List<DateTime> { new DateTime(year, 4, 1), new DateTime(year, 5, 9) },
            _ => throw new ArgumentException($"Year {year} was not expected."),
        };
    }

    [Theory]
    [InlineData(2023)]
    [InlineData(2024)]
    public void Test(int year)
    {
        var calculator = new FrenchPublicHolidaysCalculator();
        var datas = GetPholidaysDatas(year);
        var publicHolidays = calculator.GetPublicHolidays(year);

        Assert.All(datas, e => publicHolidays.Contains(e));
    }
}
