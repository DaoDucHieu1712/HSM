using HSM.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace HSM.Persistance
{
    public class EFUnitOfWorkDbContext<TContext> : IUnitOfWorkDbContext<TContext>
    where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public EFUnitOfWorkDbContext(TContext dbContext)
            => _dbContext = dbContext;

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync();

        async ValueTask IAsyncDisposable.DisposeAsync()
            => await _dbContext.DisposeAsync();
    }
}
