using System;
using System.Collections.Generic;

namespace QLDuAn.Web.Models
{
    public class HangMucViewModel
    {
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

        public int SoNguoiThucHien { get; set; }

        public int? DiemDanhGia { get; set; }

        public decimal? HesoKcn { get; set; }

        public int LoaiHangMuc { get; set; }

        public bool TrangThai { get; set; }

        public bool isDelete { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public  NhomCongViecViewModel NhomCongViec { get; set; }

        public  ApplicationUserViewModel ApplicationUser { get; set; }

        public virtual HeSoLapViewModel HeSoLap { get; set; }

        public virtual HeSoTgViewModel HeSoTg { get; set; }

        public IEnumerable<ThamGiaViewModel> ThamGia { get; set; }

    }
}