using Dashboard.API.Application.DTOs.ResponseDTOs;
using Dashboard.API.Domain.Entities;

namespace Dashboard.API.Application.Services
{
    public static class ResponseMappingExtension
    {
        public static JobApplicationResponseDTO toJobApplicationResponseDTO(this JobApplication jobApplication)
        {
            return new JobApplicationResponseDTO
            {
                Id = jobApplication.Id,
                Name = jobApplication.Name,
                Email = jobApplication.Email,
                JobTitle = jobApplication.JobTitle,
                Status = jobApplication.Status,
                CreatedAt = jobApplication.CreatedAt
            };
        }
    }
}