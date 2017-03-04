using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuAn.Model.IComponents
{
    public abstract class NgayTao : INgayTao
    {
        public DateTime? Created_at { get; set; }
   
        public DateTime? Updated_at{get;set; }
    }
}
