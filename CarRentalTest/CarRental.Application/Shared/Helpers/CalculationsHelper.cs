namespace CarRental.Application.Shared.Helpers;

/// <summary>
/// Calculations helper
/// </summary>
public static class CalculationsHelper
{
    /// <summary>
    /// Calculate price by days and hours
    /// </summary>
    /// <param name="durationInDays"></param>
    /// <param name="durationInHours"></param>
    /// <returns></returns>
    public static int CalculatePrice(double durationInDays, double durationInHours)
    {
        int price = 0;
        var fullDays = (int)durationInDays;
        var partialDay = durationInDays % 1;

        if (partialDay > 0)
        {
            price += 50;
        }

        if (fullDays != 0 )
        {
            price += fullDays * 100;
        }

        return price;
    }
}