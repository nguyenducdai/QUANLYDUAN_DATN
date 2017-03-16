using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLDuAn.Web.Models
{
    public class HangMucDuAnViewModel
    {
        public int IdDuAn { get; set; }

        public int IdHangMuc { get; set; }

        public int IdNhomCongViec { get; set; }

        public int MucDoTruyenThong { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayDuKienKetThuc { get; set; }

        public DateTime? NgayHoanThanh { get; set; }

        public string IdNguoiThucHienTheoLenhSX { get; set; }

        public decimal HeSoLapLai { get; set; }

        public decimal HeSoNhanCong { get; set; }

        public decimal HeSoCongViec { get; set; }

        public int SoNguoiThucHien { get; set; }

        public decimal HeSoThoiGian { get; set; }

        public int DiemDanhGia { get; set; }

        public int DiemHangMuc { get; set; }

        public int LoaiHangMuc { get; set; }

        public bool TrangThai { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        // public bool TrangThai { get; set; }
        //public virtual DuAnViewModel DuAn { get; set; }

        //public virtual HangMucViewModel HangMuc { get; set; }

        //public virtual NhomCongViecViewModel NhomCongViec { get; set; }

        //public virtual ApplicationUserViewModel User { get; set; }

    }
}