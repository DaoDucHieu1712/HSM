using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSM.Domain.Abstractions
{
    public interface IUnitOfWorkDbContext<TContext> : IAsyncDisposable
    {
        /// <summary>
        /// Call save change from db context
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
