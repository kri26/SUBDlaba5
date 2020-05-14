using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("number")]
    public class Number
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("typeofnumber")]
        public string TypeOfNumber { get; set; }

        [Required]
        [Column("size")]
        public int? Size { get; set; }

        public List<Orders> Orders { get; set; }
    }
}
