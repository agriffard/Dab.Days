namespace Dba.Days.Tests;

public class WorkingDaysCalculatorTests
{
    [Theory]
    [InlineData(2023, 7, 20)]
    public void Test(int year, int month, int expectedWorkingDays)
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        var workingDays = calculator.GetWorkingDaysByMonth(year, month);
        Assert.Equal(expectedWorkingDays, workingDays); // 20 jours travaillés en juillet 2023
    }

    [Fact]
    public void CheckDayOffSunday()
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        Assert.True(calculator.IsDayOff(new DateTime(2023, 07, 30))); // dimanche 30 juillet 2023
    }

    [Fact]
    public void CheckDayOffSaturday()
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        Assert.True(calculator.IsDayOff(new DateTime(2023, 07, 29))); // samedi 29 juillet 2023
    }

    [Fact]
    public void CheckDayOffHoliday()
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        Assert.True(calculator.IsDayOff(new DateTime(2023, 05, 08))); // 8 Mai ferié
    }
}
