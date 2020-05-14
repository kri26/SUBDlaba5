using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("transport")]
    public class Transport
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("numberoftransport")]
        public string NumborOfTransport { get; set; }

        [Required]
        [Column("typeoftransport")]
        public string TypeOfTransport { get; set; }

        public List<Orders> Orders { get; set; }
    }
}
