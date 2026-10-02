using Dashboard.API.Domain.Entities;
using Dashboard.API.Domain.RepositoryContracts;
using Dashboard.API.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Infrastructure.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<JobApplicationRepository> _logger;

        public JobApplicationRepository(ApplicationDbContext dbContext, ILogger<JobApplicationRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Add(JobApplication jobApplication)
        {
            _logger.LogInformation("Adding a new job application: {JobApplication}", jobApplication);
            _dbContext.JobApplications.Add(jobApplication);
            //here is marked as Add in tracker,no query executed
        }

        public async Task<JobApplication?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving job application by ID: {Id}", id);
            return await _dbContext.JobApplications.FindAsync(id);
        }

        public async Task<List<JobApplication>> GetListAsync()
        {
            _logger.LogInformation("Retrieving list of job applications");
            return await _dbContext.JobApplications
               .AsNoTracking()//very important !!!
               .OrderBy(j => j.Status)
               .ThenBy(c => c.CreatedAt)
               .ToListAsync();
        }
    }
}