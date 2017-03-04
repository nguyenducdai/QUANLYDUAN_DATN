using QLDuAn.Model.IComponents;
using System.ComponentModel.DataAnnotations.Schema;
namespace QLDuAn.Model.Models
{
    [Table("Errors")]
    public class Errors : NgayTao
    {
        public int ID { get; set; }

        public string Message { get; set; }

        public string Stacktrace { get; set; }
    }
}