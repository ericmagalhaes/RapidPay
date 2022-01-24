using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RapidPay.Repository.Entities;

namespace RapidPay.Repository
{
    public interface IRapidPayDbContext
    {
        DbSet<Card> Cards { get; set; }
        DbSet<Balance> Balances { get; set; }
        Task<int> CommitChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}