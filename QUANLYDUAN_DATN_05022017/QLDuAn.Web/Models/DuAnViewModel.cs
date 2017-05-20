using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Web.Models
{
    public class DuAnViewModel
    {
        public int ID { get; set; }

        [Required]
        public string SoHopDong { get; set; }

        public decimal GiaTriHopDong { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public DateTime? NgayHoanThanh { get; set; }

        public DateTime NgayKy { get; set; }

        public int IdKhachHang { get; set; }

        public string TenDuAn { get; set; }

        public string MoTa { get; set; }

        public DateTime NamQuyetToan { get; set; }

        public int LoaiCongTrinh { get; set; }

        public decimal TyLeTheoDT { get; set; }

        public int? LuongThueNgoai { get; set; }

        public decimal? TongChiQL { get; set; }

        public decimal? LuongTTQtt { get; set; }

        public decimal? LuongDPQdp { get; set; }

        public decimal? LuongGTQgt { get; set; }

        public decimal? LuongGTV21 { get; set; }

        public decimal? LuongGTV22 { get; set; }

        public decimal? TongDiemTT { get; set; }

        public decimal? TongDiemGT { get; set; }

        public decimal? DonGiaDiemTT { get; set; }

        public decimal? DonGiaDiemGT { get; set; }

        public int TrangThai { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public IEnumerable<ThamGiaViewModel> ThamGia { get; set; }

        public IEnumerable<HangMucViewModel> HangMuc { get; set; }

        public KhachHangViewModel KhachHang { get; set; }
    }
}