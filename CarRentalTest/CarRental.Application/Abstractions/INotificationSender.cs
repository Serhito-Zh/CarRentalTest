using CarRental.Domain.Entities;

namespace CarRental.Application.Abstractions;

public interface INotificationSender
{
    Task SendAsync(Notification notification);
}