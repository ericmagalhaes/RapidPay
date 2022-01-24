using System;
using System.Threading.Tasks;
using RapidPay.Domain.Commands;
using RapidPay.Domain.Models;

namespace RapidPay.Domain.Adapters.Interfaces
{
    public interface ICardAdapter
    {
        Task<CardCreatedModel> CreateAsync(CreateCardCmd command);
        Task<CardPaymentStatusModel> PaymentAsync(PaymentCardCmd command);
        Task<CardBalanceModel> BalanceAsync(Guid cardNumber);
    }
}