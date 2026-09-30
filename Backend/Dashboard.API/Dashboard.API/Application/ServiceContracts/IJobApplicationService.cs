using Dashboard.API.Application.DTOs.RequestDTOs;
using Dashboard.API.Application.DTOs.ResponseDTOs;
using Dashboard.API.Domain.ResultErrorDomain;

namespace Dashboard.API.Application.ServiceContracts
{
    public interface IJobApplicationService
    {
        Task<Result<List<JobApplicationResponseDTO>>> GetJobApplicationListAsync();

        Task<Result<JobApplicationResponseDTO>> CreateJobApplicationAsync(CreateJobApplicationRequestDTO request);

        Task<Result<JobApplicationResponseDTO>> UpdateJobApplicationByIdAsync(UpdateJobApplicationRequestDTO request);
    }
}