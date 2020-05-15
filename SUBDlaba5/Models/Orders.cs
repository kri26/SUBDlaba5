using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("orders")]
    public class Orders
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("databegin")]
        public DateTime? DataBegin { get; set; }

        [Required]
        [Column("dataend")]
        public DateTime? DataEnd { get; set; }

        [Required]
        [Column("customersid")]
        public int? CustomersId { get; set; }

        [Required]
        [Column("moneyid")]
        public int? MoneyId { get; set; }

        [Required]
        [Column("workerid")]
        public int? WorkerId { get; set; }

        [Required]
        [Column("transportid")]
        public int? TransportId { get; set; }

        [Required]
        [Column("numberid")]
        public int? NumberId { get; set; }

        public Customer Customers { get; set; }
        public Money Money { get; set; }
        public Worker Worker { get; set; }
        public Transport Transport { get; set; }
        public Number Number { get; set; }
    }
}
