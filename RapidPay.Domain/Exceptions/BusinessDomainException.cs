using System;

namespace RapidPay.Domain.Exceptions
{
    public class BusinessDomainException : ApplicationException
    {
        public BusinessDomainException(string businessMessage, Exception ex) : base(ex.Message,ex)
        {
            
        }
    }
}