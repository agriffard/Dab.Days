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
    //[InlineData(2023, 5, 1, 2023, 5, 31, 20)]
    //[InlineData(2023, 6, 1, 2023, 6, 30, 22)]
    //[InlineData(2023, 7, 1, 2023, 7, 31, 20)]
    [InlineData(2024, 12, 22, 2025, 1, 2, 7)]
    [InlineData(2025, 5, 1, 2025, 5, 31, 19)]
    public void GetWorkingDaysBetweenDates_ShouldReturnCorrectCount(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay, int expectedWorkingDays)
    {
        // Arrange
        var startDate = new DateTime(startYear, startMonth, startDay);
        var endDate = new DateTime(endYear, endMonth, endDay);
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
        workingDays.Should().Be(expectedWorkingDays); // 20 jours travaillés en juillet 2023
    }

    #endregion

    #region IsDayOff

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

    #endregion
}
