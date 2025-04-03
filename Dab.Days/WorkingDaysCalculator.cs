using System.Collections.Concurrent;

namespace Dab.Days;

public class WorkingDaysCalculator : IWorkingDaysCalculator
{
    private readonly IPublicHolidaysCalculator _publicHolidaysCalculator;
    private readonly IDictionary<int, IEnumerable<DateTime>> _publicHolidays;

    public WorkingDaysCalculator(IPublicHolidaysCalculator publicHolidaysCalculator)
    {
        _publicHolidaysCalculator = publicHolidaysCalculator;
        _publicHolidays = new ConcurrentDictionary<int, IEnumerable<DateTime>>();
    }

    public int GetWorkingDaysByMonth(int year, int month, bool workingOnSaturday = false)
    {
        if (!_publicHolidays.ContainsKey(year))
            _publicHolidays[year] = _publicHolidaysCalculator.GetPublicHolidays(year);
        var publicHolidays = _publicHolidays[year];
        var currentDay = new DateTime(year, month, 01);
        var currentMonth = month;
        var workingDays = 0;
        while (currentMonth == month)
        {
            //if neither sunday, neither saturday (not working), neither public holiday
            if (currentDay.DayOfWeek != DayOfWeek.Sunday
                && currentDay.DayOfWeek != DayOfWeek.Saturday
                && !workingOnSaturday
                && !publicHolidays.Any(h => h.Month == month && h.Day == currentDay.Day))
                workingDays++;

            currentDay = currentDay.AddDays(1);
            currentMonth = currentDay.Month;
        }
        return workingDays;
    }

    public int GetWorkingDaysBetweenDates(DateTime startDate, DateTime endDate, bool workingOnSaturday = false)
    {
        var workingDays = 0;
        var currentDay = startDate;
        var isSameYear = startDate.Year == endDate.Year;
        var publicHolidays = _publicHolidaysCalculator.GetPublicHolidays(startDate.Year);
        if (!isSameYear)
            publicHolidays = publicHolidays.Concat(_publicHolidaysCalculator.GetPublicHolidays(endDate.Year));
        while (currentDay <= endDate)
        {
            //if neither sunday, neither saturday (not working), neither public holiday
            if (currentDay.DayOfWeek != DayOfWeek.Sunday
                && currentDay.DayOfWeek != DayOfWeek.Saturday
                && !workingOnSaturday
                && !publicHolidays.Any(h => h.Year == currentDay.Year && h.Month == currentDay.Month && h.Day == currentDay.Day))
                workingDays++;
            currentDay = currentDay.AddDays(1);
        }

        return workingDays;
    }

    public virtual bool IsDayOff(DateTime dt)
    {
        var year = dt.Year;
        if (!_publicHolidays.ContainsKey(year))
            _publicHolidays[year] = _publicHolidaysCalculator.GetPublicHolidays(year);

        return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || _publicHolidays[year].Contains(dt);
    }
}
