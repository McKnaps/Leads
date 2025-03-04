
using FluentValidation;
using Leads.DTOs.AgentDTOs;

namespace Leads.Validators;

public class AgentCreateDTOValidator : AbstractValidator<AgentUpdateDTO>
{
    public AgentCreateDTOValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.OperatingArea)
            .NotEmpty().WithMessage("Operating area is required.")
            .MaximumLength(100).WithMessage("Operating area cannot exceed 100 characters.");
    }
}