using QLDuAn.Model.IComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("HangMuc")]
    public class HangMuc:NgayTao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int IdDuAn { get; set; }

        public string TenHangMuc { get; set; }

        public string IdNguoiThucHienTheoLenhSX { get; set; }

        public string MoTaHangMuc { get; set; }

        public int? IdNhomCongViec { get; set; }

        public int? IdMucDoTruyenThong { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public int? ThoiGianDuKien { get; set; }

        public DateTime? NgayHoanThanh { get; set; }

        public int? SoNguoiThucHien { get; set; }

        public int? DiemDanhGia { get; set; }

        public decimal? HesoKcn { get; set; }

        public int LoaiHangMuc { get; set; }

        public bool TrangThai { get; set; }

        public bool isDelete { get; set; }

        [ForeignKey("IdDuAn")]
        public virtual DuAn DuAn { get; set; }

        [ForeignKey("IdNhomCongViec")]
        public virtual NhomCongViec NhomCongViec { get; set; }

        [ForeignKey("IdNguoiThucHienTheoLenhSX")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("IdMucDoTruyenThong")]
        public virtual HeSoLap HeSoLap { get; set; }

        [ForeignKey("ThoiGianDuKien")]
        public virtual HeSoTg HeSoTg { get; set; }

        public virtual ICollection<ThamGia> ThamGia { get; set; }

    }
}