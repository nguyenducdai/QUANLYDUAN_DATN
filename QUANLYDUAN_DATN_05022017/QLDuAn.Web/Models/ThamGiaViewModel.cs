using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QLDuAn.Web.Models
{
    public class ThamGiaViewModel
    {
        public int Id { get; set; }
        public int IdDuAn { get; set; }

        public int IdHangMuc { get; set; }

        public string IdNhanVien { get; set; }

        public int LoaiHangMuc { get; set; }

        public decimal HeSoThamGia { get; set; }

        public decimal? ThuNhap { get; set; }

        public decimal? DiemThanhVien { get; set; }

        public virtual DuAnViewModel DuAn { get; set; }

        public virtual HangMucDuAnViewModel HangMuc { get; set; }

        public virtual ApplicationUserViewModel ApplicationUser { get; set; }
    }
}