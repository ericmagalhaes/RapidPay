using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RapidPay.Repository.Entities;

namespace RapidPay.Repository
{
    public class RapidPayDbContext : DbContext, IRapidPayDbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Balance> Balances { get; set; }

        
        public RapidPayDbContext(DbContextOptions<RapidPayDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cardBuilder = modelBuilder.Entity<Card>().ToTable("Card");
            
            cardBuilder.HasKey(c => c.Id);
            cardBuilder.Property(c => c.Number).IsRequired();
            cardBuilder.Property(c => c.Owner).IsRequired();
            cardBuilder.Property(c => c.Created).IsRequired();
            cardBuilder.Property(c => c.Modified).IsRequired();
            cardBuilder.Property(c => c.CreatedBy).IsRequired();
            cardBuilder.Property(c => c.ModifiedBy).IsRequired();
            
            
            var balanceBuilder = modelBuilder.Entity<Balance>().ToTable("Balance");
            balanceBuilder.Property(c => c.Credit).IsRequired();
            balanceBuilder.Property(c => c.Amount).IsRequired();
            balanceBuilder.Property(c => c.Created).IsRequired();
            balanceBuilder.Property(c => c.Modified).IsRequired();
            balanceBuilder.Property(c => c.CreatedBy).IsRequired();
            balanceBuilder.Property(c => c.ModifiedBy).IsRequired();
            
        }

        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Database.BeginTransactionAsync(cancellationToken);
        }
        
    }
}