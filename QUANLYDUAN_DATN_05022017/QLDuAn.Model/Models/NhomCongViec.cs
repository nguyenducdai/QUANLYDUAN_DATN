using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("NhomCongViec")]
    public class NhomCongViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string NhomCV { get; set; }

        public decimal HeSoCV { get; set; }

        public string GhiChu { get; set; }

        public virtual IEnumerable<DuAnHangMuc> DuAnHangMuc { get; set; }


    }
}