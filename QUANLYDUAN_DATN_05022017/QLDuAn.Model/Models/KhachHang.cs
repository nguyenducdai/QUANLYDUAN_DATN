using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("khachHang")]
    public class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string TenKhach { get; set; }

        [Required]
        public int SoDienThoai { get; set; }

        public string SoFax { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string DiaChi { get; set; }

        public bool GioiTinh { get; set; }

        public virtual IEnumerable<HopDong> HopDong { get; set; }
    }
}