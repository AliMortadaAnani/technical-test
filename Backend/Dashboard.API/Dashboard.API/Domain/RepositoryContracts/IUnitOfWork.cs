namespace Dashboard.API.Domain.RepositoryContracts
{
    public interface IUnitOfWork : IDisposable
    {
        // this unit of work allow us to call save changes inside services
        // and not inside repositories,
        //also it allows us to have a single transaction for multiple changes (in large services)
        Task<int> SaveChangesAsync();
    }
}