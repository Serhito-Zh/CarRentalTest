using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

/// <summary>
/// Settings for sorting/paging
/// </summary>
/// <param name="pageSize"></param>
/// <param name="order"></param>
public sealed record ReportSettings(PageSize limit, PageSize offset, Order order);
