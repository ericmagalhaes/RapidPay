using System;

namespace RapidPay.Domain.Models
{
    public class CardBalanceModel {
        public decimal Amount { get; set; }
        public Guid CardId { get; set; }
    }
}