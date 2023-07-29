namespace Dba.Days;

public interface IPublicHolidaysCalculator
{
    IEnumerable<DateTime> GetPublicHolidays(int year);
}
