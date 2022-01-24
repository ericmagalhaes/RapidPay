using System;

namespace RapidPay.Shared
{
    public interface IApplicationUser
    {
        Guid UserId { get; set; }
    }
}