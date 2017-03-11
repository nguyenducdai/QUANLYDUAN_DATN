using QLDuAn.Model.IComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuAn.Model.Models
{
    [Table("HopDong")]
    public class HopDong:NgayTao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string SoHopDong { get; set; }

        public string TenHopDong { get; set; }

        public string GiaTriHopDong { get; set; }

        public string SoNgayThucHien { get; set; }

        public string NoiDung { get; set; }
         
        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public DateTime NgayKy { get; set; }

        public int IdKhachHang { get; set; }
   
        [ForeignKey("IdKhachHang")]
        public  virtual KhachHang KhachHang { get; set; }

        public IEnumerable<DuAn> DuAn { set; get; }
    }
}
