using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("customer")]
    public class Customer
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("fullnamecustomers")]
        public string FullNameCustomers { get; set; }

        [Required]
        [Column("numberofman")]
        public int? NumberOfMan { get; set; }

        public List<Orders> Orders { get; set; }
    }
}
