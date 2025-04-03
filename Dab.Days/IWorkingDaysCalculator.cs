namespace Dab.Days;

public interface IWorkingDaysCalculator
{
    int GetWorkingDaysByMonth(int year, int month, bool workingOnSaturday = false);
    int GetWorkingDaysBetweenDates(DateTime startDate, DateTime endDate, bool workingOnSaturday = false);
    bool IsDayOff(DateTime dt);
}
