using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab5.Models
{
    [Table("worker")]
    public class Worker
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("fullnameworker")]
        public string FullNameWorker { get; set; }

        [Required]
        [Column("position")]
        public string Position { get; set; }

        public List<Orders> Orders { get; set; }
    }
}
