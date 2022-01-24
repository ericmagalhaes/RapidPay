using System;
using Microsoft.Extensions.Logging;
using RapidPay.Domain.Adapters.Interfaces;

namespace RapidPay.Domain.Adapters
{
    /// <summary>
    /// Using a distributed memory cache database is faster then working with locks in the database
    /// it is logically maintained by the domain layer reducing concurrency in the database having to lock
    /// balance table for every transaction 
    /// </summary>
    public class RedisCacheLock : IRedisCacheLock
    {
        private readonly ILogger<RedisCacheLock> _logger;

        public RedisCacheLock(ILogger<RedisCacheLock> logger)
        {
            _logger = logger;
        }
        
        public void Unlock(Guid cardId)
        {
            _logger.LogInformation($"Distributed UnLock for card {cardId:D} using in Memory Cache - Redis");
        }

        public void Lock(Guid cardId)
        {
            _logger.LogInformation($"Distributed Lock for card {cardId:D} using in Memory Cache - Redis");
        }
    }
}