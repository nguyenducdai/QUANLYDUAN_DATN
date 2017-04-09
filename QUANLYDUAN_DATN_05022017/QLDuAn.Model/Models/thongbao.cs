using QLDuAn.Model.IComponents;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("ThongBao")]
    public class ThongBao : NgayTao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public string TieuDe { set; get; }

        public string NoiDung { set; get; }

        public string NguoiTao { set; get; }

        [Column(TypeName = "xml")]
        public string MoreFile { set; get; }

    }
}