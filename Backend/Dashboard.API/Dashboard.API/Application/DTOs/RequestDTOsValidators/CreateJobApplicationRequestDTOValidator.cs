using Dashboard.API.Application.DTOs.RequestDTOs;
using FluentValidation;

namespace Dashboard.API.Application.DTOs.RequestDTOsValidators
{
    public class CreateJobApplicationRequestDTOValidator : AbstractValidator<CreateJobApplicationRequestDTO>
    {
        public CreateJobApplicationRequestDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
                .MinimumLength(5).WithMessage("Email must be at least 5 characters long.")
                ;
            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("Job title is required.")
                .MaximumLength(100).WithMessage("Job title cannot exceed 100 characters.")
                .MinimumLength(2).WithMessage("Job title must be at least 2 characters long.")
                ;
        }
    }
}