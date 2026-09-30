using Dashboard.API.Domain.RepositoryContracts;
using Dashboard.API.Infrastructure.DatabaseContext;

namespace Dashboard.API.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            _logger.LogInformation("UnitOfWork: Saving changes to database");
            int result = await _context.SaveChangesAsync();
            _logger.LogInformation("UnitOfWork: Changes saved successfully - Rows affected: {RowsAffected}", result);
            return result;
        }
    }
}