namespace CarRental.Application.Responses;

/// <summary>
/// Report response
/// </summary>
/// <param name="BookingNumber"></param>
/// <param name="PickUpDate"></param>
public sealed record ReportResponse(long BookingNumber, DateTime PickUpDate);