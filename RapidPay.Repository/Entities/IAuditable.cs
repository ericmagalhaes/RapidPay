using System;

namespace RapidPay.Repository.Entities
{
    public interface IAuditable
    {
        Guid Id { get; set; }
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
        Guid CreatedBy { get; set; }
        Guid ModifiedBy { get; set; }
    }
}