namespace RapidPay.Domain.Commands
{
    public class CreateCardCmd
    {
        public string Owner { get; }

        public CreateCardCmd(string owner)
        {
            Owner = owner;
        }
    }
}