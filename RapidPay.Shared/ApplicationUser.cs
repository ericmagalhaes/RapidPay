using System;

namespace RapidPay.Shared
{
    public class ApplicationUser : IApplicationUser
    {
        public Guid UserId { get; set; }
    }
}