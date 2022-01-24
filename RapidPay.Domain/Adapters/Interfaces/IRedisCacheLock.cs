using System;

namespace RapidPay.Domain.Adapters.Interfaces
{
    public interface IRedisCacheLock
    {
        void Unlock(Guid cardId);
        void Lock(Guid cardId);
    }
}