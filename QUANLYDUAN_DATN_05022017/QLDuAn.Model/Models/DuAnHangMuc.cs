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
        [Column(Order = 1)]
        public int IdDuAn { get; set; }

        [Key]
        [Column(Order = 2)]
        public int IdHangMuc { get; set; }

        [Key]
        [Column(Order = 3)]
        public int IdNhomCongViec { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayDuKienKetThuc { get; set; }

        public DateTime NgayHoanThanh { get; set; }

        public int? IdNguoiThucHienTheoLenhSX { get; set; }

        public decimal HeSoLapLai { get; set; }

        public decimal HeSoNhanCong { get; set; }

        public decimal HeSoCongViec { get; set; }

        public int SoNguoiThucHien { get; set; }

        public int SoNgayThucHien { get; set; }

        public decimal HeSoThoiGian { get; set; }

        public int DiemDanhGia{ get; set; }

        public int DiemHangMuc{ get; set; }

        [ForeignKey("IdHangMuc")]
        public virtual HangMuc HangMuc { get; set; }

        [ForeignKey("IdNhomCongViec")]
        public virtual NhomCongViec NhomCongViec { get; set; }

        [ForeignKey("IdDuAn")]
        public virtual DuAn DuAn { get; set; }

    }
}