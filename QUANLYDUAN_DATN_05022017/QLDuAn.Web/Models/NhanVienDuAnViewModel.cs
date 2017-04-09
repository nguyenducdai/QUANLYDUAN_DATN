using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDuAn.Web.Models
{
    public class NhanVienDuAnViewModel
    {

        public int IdDuAn { get; set; }


        public string IdNhanVien { get; set; }


        public DuAnViewModel DuAn { get; set; }

        public ApplicationUserViewModel ApplicationUser { get; set; }
    }
}