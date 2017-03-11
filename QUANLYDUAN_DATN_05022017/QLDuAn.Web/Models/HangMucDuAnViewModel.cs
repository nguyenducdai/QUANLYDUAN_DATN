using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDuAn.Web.Models
{
    public class HangMucDuAnViewModel
    {
        public int IdDuAn { get; set; }

        public int IdHangMuc { get; set; }

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

        public int DiemDanhGia { get; set; }

        public int DiemHangMuc { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }
    }
}