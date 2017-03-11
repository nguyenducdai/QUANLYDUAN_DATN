﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDuAn.Web.Models
{
    public class ThamGiaViewModel
    {
        public int IdDuAn { get; set; }

        public int IdHangMuc { get; set; }

        public int IdNhanVien { get; set; }

        public decimal HeSoThamGia { get; set; }

        public decimal? ThuNhap { get; set; }

        public decimal? DiemThanhVien { get; set; }
    }
}