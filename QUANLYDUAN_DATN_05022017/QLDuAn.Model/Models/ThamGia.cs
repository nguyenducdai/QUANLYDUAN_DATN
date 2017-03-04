using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("ThamGia")]
    public class ThamGia
    {
        [Key]
        [Column(Order =1)]
        public int IdDuAn { get; set; }

        [Key]
        [Column(Order = 2)]
        public int IdHangMuc { get; set; }

        [Key]
        [Column(Order = 3)]
        public int IdNhanVien { get; set; }

        public decimal HeSoThamGia { get; set; }

        [ForeignKey("IdDuAn")]
        public virtual DuAn DuAn { get; set; }

        [ForeignKey("IdHangMuc")]
        public virtual HangMuc HangMuc { get; set; }

        [ForeignKey("IdNhanVien")]
        public virtual NhanVien NhanVien { get; set; }
    }
}