using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("money")]
    public class Money
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("currency")]
        public string Currency { get; set; }

        [Required]
        [Column("amount")]
        public int? Amount { get; set; }

        [Required]
        [Column("workermoney")]
        public int? WorkerMoney { get; set; }

        public List<Orders> Orders { get; set; }
    }
}
