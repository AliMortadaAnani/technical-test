using Dashboard.API.Domain.Entities;

namespace Dashboard.API.Domain.RepositoryContracts
{
    public interface IJobApplicationRepository
    {
        Task<List<JobApplication>> GetListAsync();

        Task<JobApplication?> GetByIdAsync(int id);

        void Add(JobApplication jobApplication);

        // add is not a task since it is not an async call (just mark it as added)
        // update do not need a specific repo method (tracker will automatically update it if it detects changes)
    }
}