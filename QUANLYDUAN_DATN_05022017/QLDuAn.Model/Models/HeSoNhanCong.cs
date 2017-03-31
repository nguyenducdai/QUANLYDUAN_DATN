using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("HeSoNhanCong")]
    public class HeSoNhanCong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SoNguoiThucHien { get; set; }

        public decimal HeSoNcKcn { get; set; }
    }
}