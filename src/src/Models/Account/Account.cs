using src.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Account
{
    public class Account : IEntity<int>
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
        [Column("username")]
        public string username { get; set; }
        [Column("password")]
        public string password { get; set; }
        [Column("balance")]
        public double balance { get; set; }
    }
}
