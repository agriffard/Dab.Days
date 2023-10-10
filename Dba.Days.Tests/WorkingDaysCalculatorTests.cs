using FluentAssertions;

namespace Dba.Days.Tests;

public class WorkingDaysCalculatorTests
{
    [Theory]
    [InlineData(2023, 7, 20)]
    public void Test(int year, int month, int expectedWorkingDays)
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        var workingDays = calculator.GetWorkingDaysByMonth(year, month);
        workingDays.Should().Be(expectedWorkingDays); // 20 jours travaillés en juillet 2023
    }

    [Fact]
    public void CheckDayOffSunday()
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        calculator.IsDayOff(new DateTime(2023, 07, 30)).Should().BeTrue(); // dimanche 30 juillet 2023
    }

    [Fact]
    public void CheckDayOffSaturday()
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        calculator.IsDayOff(new DateTime(2023, 07, 29)).Should().BeTrue(); // samedi 29 juillet 2023
    }

    [Fact]
    public void CheckDayOffHoliday()
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        calculator.IsDayOff(new DateTime(2023, 05, 08)).Should().BeTrue(); // 8 Mai ferié
    }
}
