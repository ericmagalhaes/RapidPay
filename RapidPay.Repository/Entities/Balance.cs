using System;
using System.ComponentModel.DataAnnotations;

namespace RapidPay.Repository.Entities
{
    public class Balance : Auditable, IBalance
    {
        [Required] 
        public Decimal Credit { get; set; }
        
        [Required] 
        public Decimal Amount { get; set; }

        [Required]
        public Guid CardId { get; set; }
        
        [Required]
        public virtual Card Card { get; set; }
        
    }
}