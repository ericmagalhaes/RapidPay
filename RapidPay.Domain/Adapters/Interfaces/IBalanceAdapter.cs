using System;
using System.Threading.Tasks;
using RapidPay.Repository;
using RapidPay.Repository.Entities;

namespace RapidPay.Domain.Adapters.Interfaces
{
    public interface IBalanceAdapter
    {
        Task<IBalance> GetBalanceAsync(Guid cardNumber);
        Task<bool> CheckBalanceAsync(IBalance balance, decimal credit);
        Task CreditBalanceAsync(IBalance balance, decimal credit);
        void LockBalance(Guid cardId);
        void UnlockBalance(Guid cardId);
        Task<IBalance> InitialBalanceAsync(Guid cardId, decimal credit);
    }
}