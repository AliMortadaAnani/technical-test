using Dashboard.API.Domain.Enums;

namespace Dashboard.API.Application.DTOs.ResponseDTOs
{
    public class JobApplicationResponseDTO
    {
        // here also we will rely on FluentValidation to show what fields are returned (or might be null)
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? JobTitle { get; set; }

        public StatusEnum? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}