namespace CarRental.Application.Shared.Helpers;

/// <summary>
/// Datetime helper
/// </summary>
public static class DateTimeHelper
{
    private const int startHour = 8;
    private const int endHour = 17;
    private const int hoursInDay = 24;
    
    /// <summary>
    /// Check target date between two specific dates and hours
    /// </summary>
    /// <param name="targetDate"></param>
    /// <returns>true/false</returns>
    public static bool BeInWorkingHours(DateTime targetDate)
    {
        var startDate = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day).AddHours(startHour);
        var endDate = new DateTime(targetDate.Year, targetDate.Month, targetDate.Day).AddHours(endHour);

        return targetDate >= startDate && targetDate <= endDate;
    }

    public static double GetHoursBetweenTwoDates(DateTime start, DateTime end)
        => (end - start).TotalHours;

    public static double GetDaysFromHours(double hours) => hours / hoursInDay;
}