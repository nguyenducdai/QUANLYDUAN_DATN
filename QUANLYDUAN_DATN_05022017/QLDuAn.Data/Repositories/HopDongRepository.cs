using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface IHopDongRepository :IRepository<HopDong>
    {

    }
    public class HopDongRepository : RepositoryBase<HopDong>, IHopDongRepository
    {
        public HopDongRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
