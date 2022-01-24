using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RapidPay.Repository.Entities;
using RapidPay.Repository.Repositories.Interfaces;
using RapidPay.Shared;

namespace RapidPay.Repository.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly IRapidPayDbContext _db;
        private readonly IApplicationUser _applicationUser;

        public BalanceRepository(IRapidPayDbContext db, IApplicationUser applicationUser)
        {
            _db = db;
            _applicationUser = applicationUser;
        }

        public async Task<IBalance> GetBalanceAsync(Guid cardId)
        {
            var balance = await _db.Balances.OrderByDescending(c => c.Created).FirstAsync(c => c.CardId == cardId);
            return balance;
        }

       public async Task<IBalance> Create(Guid cardId, decimal balanceAmount, decimal credit)
        {
            var balance = CreateBalance(cardId,balanceAmount, credit, _applicationUser.UserId);
            var result = await _db.Balances.AddAsync(balance);
            await _db.CommitChangesAsync();
            return result.Entity;
        }

        private Balance CreateBalance(Guid cardId, decimal currentAmount,decimal credit, Guid createdBy)
        {
            var balance = new Balance()
            {
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                CreatedBy = createdBy,
                ModifiedBy = createdBy,
                Amount = currentAmount,
                Credit = credit,
                CardId = cardId
            };
            return balance;
        }

        private bool TestCreditBalance(Balance balance, decimal credit)
        {
            var currentBalance = balance.Amount;
            var calculatedBalance = currentBalance + credit;
            return calculatedBalance >= 0;
        }
    }
}