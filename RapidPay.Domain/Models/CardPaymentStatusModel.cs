namespace RapidPay.Domain.Models
{
    public class CardPaymentStatusModel
    {
        public bool IsApproved { get;  }
        public string ErrorMessage { get; }

        public CardPaymentStatusModel(bool isApproved)
        {
            IsApproved = isApproved;
        }
        public CardPaymentStatusModel(bool isApproved,string errorMessage)
        {
            IsApproved = isApproved;
            ErrorMessage = errorMessage;
        }
    }
}