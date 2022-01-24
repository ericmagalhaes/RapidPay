using System;
using System.Threading.Tasks;
using RapidPay.Repository.Entities;

namespace RapidPay.Repository.Repositories.Interfaces
{
    public interface IBalanceRepository
    {
        Task<IBalance> GetBalanceAsync(Guid cardId);
        Task<IBalance> Create(Guid cardId, decimal balanceAmount, decimal credit);
    }
}