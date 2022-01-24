using System.Threading.Tasks;
using RapidPay.Repository.Entities;

namespace RapidPay.Repository.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<ICard> CreateAsync(string cardOwner,string cardNumber);
    }
}