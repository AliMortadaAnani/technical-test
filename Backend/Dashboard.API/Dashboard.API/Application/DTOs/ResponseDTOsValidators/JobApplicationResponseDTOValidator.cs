using Dashboard.API.Application.DTOs.ResponseDTOs;
using FluentValidation;

namespace Dashboard.API.Application.DTOs.ResponseDTOsValidators
{
    public class JobApplicationResponseDTOValidator : AbstractValidator<JobApplicationResponseDTO>
    {   //we expect endpoint to return all fields
        public JobApplicationResponseDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty();
            RuleFor(x => x.JobTitle)
                .NotEmpty();
            RuleFor(x => x.Status)
                .NotNull();
            RuleFor(x => x.CreatedAt)
                .NotNull();
        }
    }
}