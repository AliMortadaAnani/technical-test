using Dashboard.API.Application.DTOs.RequestDTOs;
using FluentValidation;

namespace Dashboard.API.Application.DTOs.RequestDTOsValidators
{
    public class UpdateJobApplicationRequestDTOValidator : AbstractValidator<UpdateJobApplicationRequestDTO>
    {
        public UpdateJobApplicationRequestDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}