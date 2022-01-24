using System.ComponentModel.DataAnnotations;

namespace RapidPay.Repository.Entities
{
    public class Card : Auditable, ICard
    {
        [Required]
        public string Number { get; set; }

        [Required]
        public string Owner { get; set; }
    }
}