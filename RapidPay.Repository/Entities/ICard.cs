namespace RapidPay.Repository.Entities
{
    public interface ICard: IAuditable
    {
        string Number { get; set; }
        string Owner { get; set; }
    }
}