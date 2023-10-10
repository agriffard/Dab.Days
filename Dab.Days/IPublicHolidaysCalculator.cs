namespace Dab.Days;

public interface IPublicHolidaysCalculator
{
    IEnumerable<DateTime> GetPublicHolidays(int year);
}
