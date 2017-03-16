using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Web.Models
{
    public class DuAnViewModel
    {
        public int ID { get; set; }

        public string TenDuAn { get; set; }

        public int IdHopDong { get; set; }

        public string MoTa { get; set; }

        public int SoNgayThucHienThucTe { get; set; }

        public DateTime NamQuyetToan { get; set; }

        public int LoaiCongTrinh { get; set; }

        public decimal TyLeTheoDT { get; set; }

        public decimal? TongDiem { get; set; }

        public decimal? DonGiaDiemDiem { get; set; }

        public int? LuongThueNgoai { get; set; }

        public decimal? TongChiQL { get; set; }

        public decimal? LuongTTQtt { get; set; }

        public decimal? LuongDPQdp { get; set; }

        public decimal? LuongGTQgt { get; set; }

        public decimal? LuongGTV21 { get; set; }

        public decimal? LuongGTV22 { get; set; }

        public int TrangThai { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public virtual HopDongViewModel HopDong { get; set; }

        public virtual IEnumerable<ThamGiaViewModel> ThamGia { get; set; }

        public virtual IEnumerable<HangMucDuAnViewModel> DuAnHangMuc { get; set; }
    }
}