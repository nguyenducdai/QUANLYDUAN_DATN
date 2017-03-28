using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("ThamGia")]
    public class ThamGia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public int IdDuAn { get; set; }

        public int IdHangMuc { get; set; }

        public string IdNhanVien { get; set; }

        public int LoaiHangMuc { get; set; }

        public decimal HeSoThamGia { get; set; }

        public decimal? ThuNhap { get; set; }

        public decimal? DiemThanhVien { get; set; }

        [ForeignKey("IdDuAn")]
        public virtual DuAn DuAn { get; set; }

        [ForeignKey("IdHangMuc")]
        public virtual HangMuc HangMuc { get; set; }

        [ForeignKey("IdNhanVien")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}