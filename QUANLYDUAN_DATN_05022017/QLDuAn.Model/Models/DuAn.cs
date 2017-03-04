using QLDuAn.Model.IComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("DuAn")]
    public class DuAn : NgayTao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
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


        //0-> chưa thực hiện , 1-> đang thực hiện / 2->hoàn thành /2hoàn thành muộn
        public int TrangThai { get; set; }

        [ForeignKey("IdHopDong")]
        public virtual HopDong HopDong { get; set; }

        public virtual IEnumerable<ThamGia> ThamGia { get; set; }

    }
}