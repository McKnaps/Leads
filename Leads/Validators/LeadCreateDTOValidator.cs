using FluentValidation;
using Leads.DTOs.LeadDTOs;

namespace Leads.Validators;

public class LeadCreateDTOValidator : AbstractValidator<LeadCreateDTO>
{
    public LeadCreateDTOValidator()
    {
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location cannot be empty.")
            .MaximumLength(50).WithMessage("Location cannot exceed 50 characters.");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price cannot be empty.");
        RuleFor(x => x.PropertyType)
            .NotEmpty().WithMessage("Property type cannot be empty.");
        RuleFor(x => x.AgentId)
            .NotEmpty().WithMessage("Agent Id cannot be empty.");
        RuleFor(x => x.OwnerContact)
            .NotEmpty().WithMessage("Owner contact cannot be empty.");
    }
}