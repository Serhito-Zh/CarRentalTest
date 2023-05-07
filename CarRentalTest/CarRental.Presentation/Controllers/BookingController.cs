using System.ComponentModel.DataAnnotations;
using System.Net;
using CarRental.Application.Entities.Booking.Commands.CreateBooking;
using CarRental.Application.Entities.Booking.Commands.DeleteBooking;
using CarRental.Application.Entities.Booking.Commands.UpdateBooking;
using CarRental.Application.Entities.Booking.Queries;
using CarRental.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Presentation.Controllers;

/// <summary>
/// Booking controller
/// </summary>
[Route("/booking")]
[AllowAnonymous]
public sealed class BookingController : ApiController
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="sender"></param>
    public BookingController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Get booking details by number
    /// </summary>
    /// <param name="bookingNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBookingByNumberAsync(long bookingNumber, CancellationToken cancellationToken)
    {
        var query = new GetBookingByNumberQuery(bookingNumber);
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Create new booking
    /// </summary>
    /// <param name="pickUpDate"></param>
    /// <param name="dropOffDate"></param>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateBookingAsync(
        DateTime pickUpDate, 
        DateTime dropOffDate, 
        string email, 
        CancellationToken cancellationToken)
    {
        var command = new CreateBookingCommand(pickUpDate, dropOffDate, email);
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Update existing booking
    /// </summary>
    /// <param name="pickUpDate"></param>
    /// <param name="dropOffDate"></param>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateBookingAsync(
        DateTime pickUpDate, 
        DateTime dropOffDate, 
        string email, 
        CancellationToken cancellationToken)
    {
        var command = new UpdateBookingCommand(pickUpDate,  dropOffDate, email);
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Delete existing booking by number
    /// </summary>
    /// <param name="bookingNumber"></param>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteBookingAsync(
        long bookingNumber,
        string email, 
        CancellationToken cancellationToken)
    {
        var command = new DeleteBookingCommand(bookingNumber, email);
        var result = await Sender.Send(command, cancellationToken);

        return result == 1 ? Ok() : BadRequest();
    }
}