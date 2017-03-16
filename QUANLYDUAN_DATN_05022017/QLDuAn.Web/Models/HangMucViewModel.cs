using System;
using System.Collections.Generic;

namespace QLDuAn.Web.Models
{
    public class HangMucViewModel
    {
        public int ID { get; set; }

        public string TenHangMuc { get; set; }

        public string MoTaHangMuc { get; set; }

        public bool LoaiHangMuc { get; set; }

        public bool TrangThai { get; set; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public virtual IEnumerable<HangMucDuAnViewModel> DuAnHangMuc { get; set; }

        public virtual IEnumerable<ThamGiaViewModel> ThamGia { get; set; }
    }
}