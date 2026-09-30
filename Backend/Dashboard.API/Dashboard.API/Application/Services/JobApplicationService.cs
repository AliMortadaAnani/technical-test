using Dashboard.API.Application.DTOs.RequestDTOs;
using Dashboard.API.Application.DTOs.ResponseDTOs;
using Dashboard.API.Application.ServiceContracts;
using Dashboard.API.Domain.Entities;
using Dashboard.API.Domain.Enums;
using Dashboard.API.Domain.RepositoryContracts;
using Dashboard.API.Domain.ResultErrorDomain;

namespace Dashboard.API.Application.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<JobApplicationService> _logger;

        public JobApplicationService(IJobApplicationRepository jobApplicationRepository, IUnitOfWork unitOfWork, ILogger<JobApplicationService> logger)
        {
            _jobApplicationRepository = jobApplicationRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<JobApplicationResponseDTO>> CreateJobApplicationAsync(CreateJobApplicationRequestDTO request)
        {
            _logger.LogInformation("Create JobApplication operation initiated");
            var jobApplication = JobApplication.Create(request.Name!, request.Email!, request.JobTitle!);

            _jobApplicationRepository.Add(jobApplication);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("JobApplication created successfully with ID: {JobApplicationId}", jobApplication.Id);

            var response = jobApplication.toJobApplicationResponseDTO();

            return Result<JobApplicationResponseDTO>.Success(response);
        }

        public async Task<Result<List<JobApplicationResponseDTO>>> GetJobApplicationListAsync()
        {
            _logger.LogInformation("Fetching all job applications from the repository.");
            var jobApplications = await _jobApplicationRepository.GetListAsync();
            _logger.LogInformation("Fetched {Count} job applications.", jobApplications.Count);
            var response = jobApplications.Select(c => c.toJobApplicationResponseDTO()).ToList();
            return Result<List<JobApplicationResponseDTO>>.Success(response);
        }

        public async Task<Result<JobApplicationResponseDTO>> UpdateJobApplicationByIdAsync(UpdateJobApplicationRequestDTO request)
        {
            _logger.LogInformation("Update JobApplication operation initiated for ID: {JobApplicationId}", request.Id);

            var jobApplication = await _jobApplicationRepository.GetByIdAsync(request.Id!.Value);
            if (jobApplication == null)
            {
                _logger.LogWarning("JobApplication with ID: {JobApplicationId} not found.", request.Id);
                return Result<JobApplicationResponseDTO>.Failure(Error.NotFound(nameof(ProblemDetails404ErrorTypes.JobApplication_NotFound), "JobApplication not found"));
            }

            if (jobApplication.Status == StatusEnum.Done)
            {
                _logger.LogWarning("JobApplication with ID: {JobApplicationId} is already in Done status. No further updates allowed.", jobApplication.Id);
                return Result<JobApplicationResponseDTO>.Failure(Error.Conflict(nameof(ProblemDetails409ErrorTypes.JobApplication_AlreadyDone), "JobApplication is already in Done status"));
            }

            if (jobApplication.Status == StatusEnum.New)
            {
                _logger.LogInformation("Updating JobApplication with ID: {JobApplicationId}. Current Status: New", jobApplication.Id);
                jobApplication.MakeInProgress();
                _logger.LogInformation("JobApplication updated to InProgress successfully with ID: {JobApplicationId}", jobApplication.Id);
            }
            else if (jobApplication.Status == StatusEnum.InProgress)
            {
                _logger.LogInformation("Updating JobApplication with ID: {JobApplicationId}. Current Status: InProgress", jobApplication.Id);
                jobApplication.MakeDone();
                _logger.LogInformation("JobApplication updated to Done successfully with ID: {JobApplicationId}", jobApplication.Id);
            }
            else
            {
                _logger.LogWarning("JobApplication with ID: {JobApplicationId} is in invalid status. Contact Support.", jobApplication.Id);
                return Result<JobApplicationResponseDTO>.Failure(Error.Failure(nameof(ProblemDetails500ErrorTypes.Server_Error), "JobApplication is in invalid status"));
            }

            await _unitOfWork.SaveChangesAsync();

            var response = jobApplication.toJobApplicationResponseDTO();
            return Result<JobApplicationResponseDTO>.Success(response);
        }
    }
}