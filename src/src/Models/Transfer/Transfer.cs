using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Transfer
{
    public class Transfer
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column("created")]
        public DateTime created { get; set; }
        [Column("updated")]
        public DateTime? updated { get; set; }
        [Column("deleted")]
        public bool deleted { get; set; }
        [Column("accountFromId")]
        public int accountFromId { get; set; }
        [Column("accountToId")]
        public int accountToId { get; set; }
        [Column("ammount")]
        public double ammount { get; set; }

    }
}
