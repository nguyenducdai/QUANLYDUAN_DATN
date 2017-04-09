using System;

namespace QLDuAn.Web.Models
{
    public class HopDongViewModel
    {
        public int ID { get; set; }

        public string SoHopDong { get; set; }

        public string TenHopDong { get; set; }

        public decimal GiaTriHopDong { get; set; }

        public string SoNgayThucHien { get; set; }

        public string NoiDung { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public DateTime NgayKy { get; set; }

        public int IdKhachHang { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }
    }
}