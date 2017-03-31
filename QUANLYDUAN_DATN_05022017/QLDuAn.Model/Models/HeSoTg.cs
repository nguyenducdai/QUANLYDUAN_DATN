using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("HeSoTg")]
    public class HeSoTg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ThoiGianDk { get; set; }

        public decimal HeSoTgdk { get; set; }
    }
}