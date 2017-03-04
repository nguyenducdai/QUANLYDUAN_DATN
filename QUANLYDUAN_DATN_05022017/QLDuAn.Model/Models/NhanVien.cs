using QLDuAn.Model.IComponents;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("NhanVien")]
    public class NhanVien : NgayTao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(200)]
        public string TenNhanVien { get; set; }

        public int SoDienThoai { get; set; }

        public int ChungMinhThu { get; set; }

        public string DiaChi { get; set; }

        public bool GioiTinh { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public virtual IEnumerable<ThamGia> ThamGia { get; set; }

    }

}