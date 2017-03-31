using QLDuAn.Model.IComponents;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("DuAnHangMuc")]
    public class DuAnHangMuc : NgayTao
    {
        [Key]
        [Column(Order = 0)]
        public int IdDuAn { get; set; }

        [Key]
        [Column(Order = 1)]
        public int IdHangMuc { get; set; }

        [StringLength(128)]
        public string IdNguoiThucHienTheoLenhSX { get; set; } 

        public int IdNhomCongViec { get; set; } 

        public int IdMucDoTruyenThong { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public int ThoiGianDuKien { get; set; }

        public DateTime? NgayHoanThanh { get; set; }

        public int SoNguoiThucHien { get; set; }

        public int DiemDanhGia { get; set; }

        public int LoaiHangMuc { get; set; }

        public decimal HesoKcn { get; set; }

        public bool TrangThai { get; set; }

        [ForeignKey("IdDuAn")]
        public virtual DuAn DuAn { get; set; }

        [ForeignKey("IdHangMuc")]
        public virtual HangMuc HangMuc { get; set; }

        [ForeignKey("IdNhomCongViec")]
        public virtual NhomCongViec NhomCongViec { get; set; }

        [ForeignKey("IdNguoiThucHienTheoLenhSX")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("IdMucDoTruyenThong")]
        public virtual HeSoLap HeSoLap { get; set; }

        [ForeignKey("ThoiGianDuKien")]
        public virtual HeSoTg HeSoTg { get; set; }

    }
}