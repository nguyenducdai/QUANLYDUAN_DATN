using System;
using System.Collections.Generic;

namespace QLDuAn.Web.Models
{
    public class HangMucDuAnViewModel
    {
        public int IdDuAn { get; set; }

        public int IdHangMuc { get; set; }

        public string IdNguoiThucHienTheoLenhSX { get; set; }

        public int IdNhomCongViec { get; set; }

        public int IdMucDoTruyenThong { get; set; }

        public DateTime? NgayBatDau { get; set; }

        public int ThoiGianDuKien { get; set; }

        public DateTime? NgayHoanThanh { get; set; }

        public int SoNguoiThucHien { get; set; }

        public int DiemDanhGia { get; set; }

        public decimal HesoKcn { get; set; }

        public int LoaiHangMuc { get; set; }

        public bool TrangThai { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public IEnumerable<ThamGiaViewModel> ThamGia { get; set; }

        //public DuAnViewModel DuAn { get; set; }

        //public HangMucViewModel HangMuc { get; set; }

        //public NhomCongViecViewModel NhomCongViec { get; set; }

        //public ApplicationUserViewModel ApplicationUser { get; set; }

        //public HeSoLapViewModel HeSoLap { get; set; }

        //public HeSoTgViewModel HeSoTg { get; set; }

        //public HeSoNhanCongViewModel HeSoNhanCong { get; set; }

    }
}