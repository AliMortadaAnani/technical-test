using Dashboard.API.API.Controllers;
using Dashboard.API.Application.DTOs.RequestDTOs;
using Dashboard.API.Application.DTOs.ResponseDTOs;
using Dashboard.API.Application.ServiceContracts;
using Dashboard.API.Domain.ResultErrorDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Dashboard.API.Controllers
{
    /// <summary>
    /// Handles job application operations for the dashboard.
    /// StatusEnum: 0(New), 1(InProgress), 2(Done)
    /// </summary>
    [EnableRateLimiting("GeneralApiLimiter")]
    public class DashboardController : ApiController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IJobApplicationService _jobApplicationService;

        public DashboardController(ILogger<DashboardController> logger, IJobApplicationService jobApplicationService)
        {
            _logger = logger;
            _jobApplicationService = jobApplicationService;
        }

        /// <summary>
        /// Get the list of all job applications.
        /// StatusEnum: 0(New), 1(InProgress), 2(Done)
        /// </summary>
        /// <returns>A list of job applications.</returns>
        /// <response code="200">Returns the list of job applications.(no paging currently implemented)</response>
        /// <response code="429">Too many requests.</response>
        [HttpGet("job-applications")]
        [ProducesResponseType(typeof(List<JobApplicationResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails429ErrorTypes), StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> GetJobApplicationList()
        {
            var result = await _jobApplicationService.GetJobApplicationListAsync();
            return HandleResult(result);
        }

        /// <summary>
        /// Creates a new job application.
        /// </summary>
        /// <param name="dto">The job application data to create.</param>
        /// <returns>The newly created job application.</returns>
        /// <response code="201">Job application created successfully.</response>
        /// <response code="400">Invalid request data.(fluent validation errors / or model binding errors(types mismatch))</response>
        /// <response code="429">Too many requests.</response>
        [HttpPost("job-application")]
        [ProducesResponseType(typeof(JobApplicationResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails429ErrorTypes), StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> CreateJobApplication([FromBody] CreateJobApplicationRequestDTO dto)
        {
            var result = await _jobApplicationService.CreateJobApplicationAsync(dto);
            return HandleResult(result, Created: true);
        }

        /// <summary>
        /// Updates an existing job application's status.
        /// </summary>
        /// <param name="dto">The job application id to update.</param>
        /// <returns>The updated job application.</returns>
        /// <response code="200">Job application updated successfully.</response>
        /// <response code="400">Invalid request data.(fluent validation errors / or model binding errors(types mismatch))</response>
        /// <response code="404">Job application not found.</response>
        /// <response code="409">Invalid status transition.(calling update status for a done job application)</response>
        /// <response code="429">Too many requests.</response>
        /// <response code="500">Internal server error.(if status was not valid at all)</response>
        [HttpPut("job-application")]
        [ProducesResponseType(typeof(JobApplicationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails404ErrorTypes), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails409ErrorTypes), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails500ErrorTypes), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails429ErrorTypes), StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> UpdateJobApplication([FromBody] UpdateJobApplicationRequestDTO dto)
        {
            var result = await _jobApplicationService.UpdateJobApplicationByIdAsync(dto);
            return HandleResult(result);
        }
    }
}