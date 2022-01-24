using System;
using System.Threading.Tasks;
using RapidPay.Repository.Entities;
using RapidPay.Repository.Repositories.Interfaces;
using RapidPay.Shared;

namespace RapidPay.Repository.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly IRapidPayDbContext _db;
        private readonly IApplicationUser _applicationUser;

        public CardRepository(IRapidPayDbContext db,IApplicationUser applicationUser)
        {
            _db = db;
            _applicationUser = applicationUser;
        }

        public async Task<ICard> CreateAsync(string cardOwner,string cardNumber)
        {
            var cardEntity = InternalCreateCard(cardOwner, cardNumber,_applicationUser.UserId);
            
            var cardEntityEntry  = await _db.Cards.AddAsync(cardEntity);
            await _db.CommitChangesAsync();

            return cardEntityEntry.Entity;
        }

        private Card InternalCreateCard(string cardOwner,string cardNumber, Guid createdBy)
        {
            var card = new Card()
            {
                Id = Guid.Parse(cardNumber),
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow,
                CreatedBy = createdBy,
                ModifiedBy = createdBy,
                Number = cardNumber,
                Owner = cardOwner
            };
            return card;

        }
        
    }
}