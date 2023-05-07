using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using CarRental.Application.Exceptions;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace CarRental.Application.PipelineBehaviours;

/// <inheritdoc />
public sealed class ValidationPipelineBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="validators"></param>
    public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators) 
        => _validators = validators;

    /// <summary>
    /// Validation handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="next"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        
        var errors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages)
                    => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
            .ToDictionary(x => x.Key, x => x.Values);
        
            if (errors.Any())
            {
                throw new CustomValidationException(errors);
            }

        return await next();
    }
}