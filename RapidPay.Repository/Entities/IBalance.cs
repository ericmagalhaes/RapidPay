using System;

namespace RapidPay.Repository.Entities
{
    public interface IBalance : IAuditable
    {
        Decimal Credit { get; set; }
        Decimal Amount { get; set; }
        Guid CardId { get; set; }
    }
}