using QLDuAn.Model.IComponents;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("HangMuc")]
    public class HangMuc:NgayTao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string TenHangMuc { get; set; }

        public string MoTaHangMuc { get; set; }

        public bool LoaiHangMuc { get; set; }

        public bool TrangThai { get; set; }

        public virtual ICollection<DuAnHangMuc> DuAnHangMuc { get; set; }

        public virtual ICollection<ThamGia> ThamGia { get; set; }

    }
}