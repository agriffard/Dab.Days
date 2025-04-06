//using Moq;

namespace Dab.Days.Tests;

public class WorkingDaysCalculatorTests
{
    //private readonly Mock<IPublicHolidaysCalculator> _publicHolidaysCalculatorMock;
    private readonly IPublicHolidaysCalculator _publicHolidaysCalculator;
    private readonly WorkingDaysCalculator _workingDaysCalculator;

    public WorkingDaysCalculatorTests()
    {
        //_publicHolidaysCalculatorMock = new Mock<IPublicHolidaysCalculator>();
        _publicHolidaysCalculator = new FrenchPublicHolidaysCalculator();
        _workingDaysCalculator = new WorkingDaysCalculator(_publicHolidaysCalculator); // _publicHolidaysCalculatorMock.Object);
    }

    #region GetWorkingDaysBetweenDates

    [Theory]
    //[InlineData(2023, 5, 1, 0, 2023, 5, 31, 0, 20)]
    //[InlineData(2023, 6, 1, 0, 2023, 6, 30, 0, 22)]
    //[InlineData(2023, 7, 1, 0, 2023, 7, 31, 0, 20)]
    //[InlineData(2024, 12, 22, 0, 2025, 1, 2, 0, 7)]
    //[InlineData(2024, 12, 22, 12, 2025, 1, 2, 12, 6)]
    //[InlineData(2024, 12, 22, 12, 2025, 1, 2, 0, 6.5)]
    //[InlineData(2024, 12, 22, 0, 2025, 1, 2, 12, 6.5)]
    //[InlineData(2025, 5, 1, 0, 2025, 5, 31, 0, 19)]
    //[InlineData(2025, 4, 7, 0, 2025, 4, 7, 0, 1)] // 1 jour
    //[InlineData(2025, 4, 7, 0, 2025, 4, 7, 12, 0.5)] // 1 matin
    [InlineData(2025, 4, 7, 12, 2025, 4, 7, 0, 0.5)] // 1 après-midi
    //[InlineData(2025, 4, 7, 12, 2025, 4, 8, 12, 1)] // 1 après-midi et 1 matin
    //[InlineData(2025, 4, 7, 12, 2025, 4, 8, 0, 1.5)] // 1 après-midi et 1 jour
    //[InlineData(2025, 4, 7, 0, 2025, 4, 8, 12, 1.5)] // 1 jour et 1 matin
    //[InlineData(2025, 4, 7, 0, 2025, 4, 8, 0, 2)] // 2 jours
    public void GetWorkingDaysBetweenDates_ShouldReturnCorrectCount(int startYear, int startMonth, int startDay, int startHours, int endYear, int endMonth, int endDay, int endHours, double expectedWorkingDays)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay, startHours, 0, 0);
        var endDate = new DateTime(endYear, endMonth, endDay, endHours, 0, 0);
        //var publicHolidays = new List<DateTime> { new DateTime(2023, 5, 1) };
        //_publicHolidaysCalculatorMock.Setup(x => x.GetPublicHolidays(2023)).Returns(publicHolidays);

        // Act
        var result = _workingDaysCalculator.GetWorkingDaysBetweenDates(startDate, endDate);

        // Assert
        Assert.Equal(expectedWorkingDays, result);
    }

    #endregion

    #region GetWorkingDaysByMonth

    [Theory]
    [InlineData(2023, 7, 20)]
    public void Test(int year, int month, int expectedWorkingDays)
    {
        var calculator = new WorkingDaysCalculator(new FrenchPublicHolidaysCalculator());
        var workingDays = calculator.GetWorkingDaysByMonth(year, month);
        Assert.Equal(expectedWorkingDays, workingDays); // 20 jours travaillés en juillet 2023
    }

    #endregion

    #region IsDayOff

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
        Assert.True(calculator.IsDayOff(new DateTime(2023, 05, 08))); // 8 Mai férié
    }

    #endregion
}
