using RapidPay.Domain.Commands;
using RapidPay.Repository;

namespace RapidPay.Domain.Models
{
    public class CardCreatedModel
    {
        public string CardOwner { get; }
        public string CardNumber { get; }
        
        public decimal Balance { get; }

        public CardCreatedModel(string cardOwner, string cardNumber, decimal balance)
        {
            CardOwner = cardOwner;
            CardNumber = cardNumber;
            Balance = balance;
        }
    }
}