using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RapidPay.Domain.Adapters.Interfaces;
using RapidPay.Domain.Commands;
using RapidPay.Domain.Exceptions;
using RapidPay.Domain.Models;
using RapidPay.Repository;
using RapidPay.Repository.Entities;
using RapidPay.Repository.Repositories.Interfaces;

namespace RapidPay.Domain.Adapters
{
    public class CardAdapter : ICardAdapter
    {
        private readonly IRapidPayDbContext _db;
        private readonly ICardRepository _cardRepository;
        private readonly IBalanceAdapter _balanceAdapter;
        private readonly UniversalFeesExchange _feesExchange;
        private readonly ILogger<CardAdapter> _logger;
        private const string ThereIsNoBalanceForThisOperation = nameof(ThereIsNoBalanceForThisOperation);
        private const string CannotCompleteYourRequest = nameof(CannotCompleteYourRequest);

        public CardAdapter(
            IRapidPayDbContext db,
            ICardRepository cardRepository, 
            IBalanceAdapter balanceAdapter, 
            UniversalFeesExchange feesExchange,
            ILogger<CardAdapter> logger)
        {
            _db = db;
            _cardRepository = cardRepository;
            _balanceAdapter = balanceAdapter;
            _feesExchange = feesExchange;
            _logger = logger;
        }

        public async Task<CardCreatedModel> CreateAsync(CreateCardCmd command)
        {
            var cardOwner = command.Owner;
            var cardNumber = CardNumberGenerator();
            ICard card = null;
            IBalance balance = null;
            using var transaction = await _db.BeginTransactionAsync();
            try
            {
                card = await _cardRepository.CreateAsync(cardOwner, cardNumber);
                balance = await _balanceAdapter.InitialBalanceAsync(card.Id, 0);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new BusinessDomainException(CannotCompleteYourRequest, ex);
            }
            var cardCreatedModel = new CardCreatedModel(card.Owner, card.Number, balance.Amount);
            return cardCreatedModel;
        }

        public async Task<CardPaymentStatusModel> PaymentAsync(PaymentCardCmd command)
        {
            var cardId = command.CardId;
            var credit = command.Credit;
            var creditWithFee = credit + _feesExchange.Instance.CurrentFee();

            _balanceAdapter.LockBalance(cardId);
            var balance = await _balanceAdapter.GetBalanceAsync(cardId);
            var hasFunds = await _balanceAdapter.CheckBalanceAsync(balance, creditWithFee);
            if (!hasFunds)
            {
                _balanceAdapter.UnlockBalance(cardId);
                return new CardPaymentStatusModel(hasFunds,ThereIsNoBalanceForThisOperation);
            }
            await _balanceAdapter.CreditBalanceAsync(balance, credit);
            _balanceAdapter.UnlockBalance(cardId);
            return new CardPaymentStatusModel(hasFunds);
        }

        public async Task<CardBalanceModel> BalanceAsync(Guid cardId)
        {
            var balance =  await _balanceAdapter.GetBalanceAsync(cardId);
            return new CardBalanceModel()
            {
                Amount = balance.Amount,
                CardId = balance.CardId
            };
        }

        private string CardNumberGenerator()
        {
            return Guid.NewGuid().ToString("D").ToUpper();
        }
    }
}