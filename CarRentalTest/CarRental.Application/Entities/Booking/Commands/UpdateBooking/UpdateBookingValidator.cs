using CarRental.Application.Shared.Helpers;
using FluentValidation;

namespace CarRental.Application.Entities.Booking.Commands.UpdateBooking;

public sealed class UpdateBookingValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingValidator()
    {
        RuleFor(x => x.pickUpDate)
            .NotEmpty()
            .GreaterThan(p => DateTime.UtcNow)
            .WithMessage($"Date should be greater than { DateTime.UtcNow:yyyy-MM-dd:h:mm:ss}")
            .Must(DateTimeHelper.BeInWorkingHours)
            .WithMessage("Pickup time must be between 8am – 5pm");
        
        RuleFor(x => x.dropOffDate)
            .NotEmpty()
            .GreaterThan(p => DateTime.UtcNow)
            .WithMessage($"Date should be greater than { DateTime.UtcNow:yyyy-MM-dd:h:mm:ss}")
            .Must(DateTimeHelper.BeInWorkingHours)
            .WithMessage("Drop off time must be between 8am – 5pm");
        
        RuleFor(x => x.email).NotEmpty().EmailAddress();
    }
}