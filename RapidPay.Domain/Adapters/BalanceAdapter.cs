using System;
using System.Threading.Tasks;
using RapidPay.Domain.Adapters.Interfaces;
using RapidPay.Repository;
using RapidPay.Repository.Entities;
using RapidPay.Repository.Repositories.Interfaces;

namespace RapidPay.Domain.Adapters
{
    public class BalanceAdapter : IBalanceAdapter
    {
        private readonly IBalanceRepository _balanceRepository;
        private readonly IRedisCacheLock _redisCacheLock;

        public BalanceAdapter(IBalanceRepository balanceRepository,IRedisCacheLock redisCacheLock)
        {
            _balanceRepository = balanceRepository;
            _redisCacheLock = redisCacheLock;
        }

        public async Task<IBalance> GetBalanceAsync(Guid cardId)
        {
            return await _balanceRepository.GetBalanceAsync(cardId);
        }
        

        public async Task<bool> CheckBalanceAsync(IBalance balance, decimal credit)
        {
            var currentBalance = balance.Amount;
            var calculatedBalance = currentBalance + credit;
            return calculatedBalance >= 0;
        }

        

        public async Task<IBalance> InitialBalanceAsync(Guid cardId, decimal credit)
        {
            var balance = await _balanceRepository.Create(cardId, 0, 0);
            return balance;
        }
        public async Task CreditBalanceAsync(IBalance balance, decimal credit)
        {
            var creditedAmount = balance.Amount + credit;
            await _balanceRepository.Create(balance.CardId, creditedAmount, credit);

        }

        //distributed lock solution using redis
        public void LockBalance(Guid cardId)
        {
            // thread wait until it is release
            _redisCacheLock.Lock(cardId);
        }
        
        //distributed unlock solution using redis
        public void UnlockBalance(Guid cardId)
        {
            _redisCacheLock.Unlock(cardId);
        }
    }
}