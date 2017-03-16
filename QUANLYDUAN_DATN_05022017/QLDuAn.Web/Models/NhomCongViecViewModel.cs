using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDuAn.Web.Models
{
    public class NhomCongViecViewModel
    {
        public int ID { get; set; }

        public string NhomCV { get; set; }

        public decimal HeSoCV { get; set; }

        public string GhiChu { get; set; }

        public virtual IEnumerable<HangMucDuAnViewModel> DuAnHangMuc { get; set; }

    }
}