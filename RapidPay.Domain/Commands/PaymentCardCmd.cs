using System;

namespace RapidPay.Domain.Commands
{
    public class PaymentCardCmd
    {
        public PaymentCardCmd(Guid cardId, decimal credit)
        {
            CardId = cardId;
            Credit = credit;
        }

        public Guid CardId { get;  }
        public decimal Credit { get; }
    }
}