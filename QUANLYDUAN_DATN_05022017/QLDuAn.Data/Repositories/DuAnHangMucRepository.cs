using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
namespace QLDuAn.Data.Repositories
{
    public interface IDuAnHangMucRepository : IRepository<DuAnHangMuc>
    {

    }
    public class DuAnHangMucRepository : RepositoryBase<DuAnHangMuc>, IDuAnHangMucRepository
    {
        public DuAnHangMucRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
