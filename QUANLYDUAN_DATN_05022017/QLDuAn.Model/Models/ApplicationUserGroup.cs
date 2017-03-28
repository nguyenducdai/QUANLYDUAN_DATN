using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDuAn.Model.Models
{
    [Table("ApplicationUserGroups")]
    public class ApplicationUserGroup
    {
        [Key]
        [Column(Order = 1)]
        public int IdGroup { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(128)]
        public string IdUser { get; set; }

        [ForeignKey("IdGroup")]
        public virtual ApplicationGroup ApplicationGroup { get; set; }

        [ForeignKey("IdUser")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}