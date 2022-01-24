using System;
using System.ComponentModel.DataAnnotations;

namespace RapidPay.Repository.Entities
{
    public class Auditable : IAuditable
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime? Modified { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [Required]
        public Guid ModifiedBy { get; set; }
    }
}