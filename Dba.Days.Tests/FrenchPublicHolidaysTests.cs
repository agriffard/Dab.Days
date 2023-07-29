namespace Dba.Days.Tests;

public class FrenchPublicHolidaysTests
{
    public static IEnumerable<DateTime> GetPholidaysDatas(int year)
    {
        return year switch
        {
            2017 => new List<DateTime> { new DateTime(year, 4, 17), new DateTime(year, 05, 05), new DateTime(year, 05, 25) },
            2016 => new List<DateTime> { new DateTime(year, 3, 28), new DateTime(year, 05, 05), new DateTime(year, 05, 16) },
            2018 => new List<DateTime> { new DateTime(year, 4, 2), new DateTime(year, 05, 10), new DateTime(year, 05, 21) },
            2019 => new List<DateTime> { new DateTime(year, 4, 22), new DateTime(year, 05, 30), new DateTime(year, 06, 10) },
            _ => throw new ArgumentException($"Year {year} was not expected."),
        };
    }

    [Theory]
    [InlineData(2017)]
    [InlineData(2016)]
    [InlineData(2018)]
    [InlineData(2019)]
    public void Test(int year)
    {
        var calculator = new FrenchPublicHolidaysCalculator();
        var datas = GetPholidaysDatas(year);
        var publicHolidays = calculator.GetPublicHolidays(year);
        Assert.All(datas, e => publicHolidays.Contains(e));
    }
}
