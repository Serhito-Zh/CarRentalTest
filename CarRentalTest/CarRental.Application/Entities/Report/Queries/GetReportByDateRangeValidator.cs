using FluentValidation;

namespace CarRental.Application.Entities.Report.Queries;

public class GetReportByDateRangeValidator : AbstractValidator<GetReportByDateRangeQuery>
{
    public GetReportByDateRangeValidator()
    {
        RuleFor(x => x.startDate).NotEmpty();
        
        RuleFor(x => x.endDate).NotEmpty();

        RuleFor(x => x.settigns.limit).IsInEnum();
        
        RuleFor(x => x.settigns.offset).IsInEnum();
        
        RuleFor(x => x.settigns.order).IsInEnum();
    }
}