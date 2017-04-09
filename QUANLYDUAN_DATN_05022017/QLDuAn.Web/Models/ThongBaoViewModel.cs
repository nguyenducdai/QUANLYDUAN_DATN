using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDuAn.Web.Models
{
    public class ThongBaoViewModel
    {
        public int Id { set; get; }

        public string TieuDe { set; get; }

        public string NoiDung { set; get; }

        public string NguoiTao { set; get; }

        public string MoreFile { set; get; }

        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }
    }
}