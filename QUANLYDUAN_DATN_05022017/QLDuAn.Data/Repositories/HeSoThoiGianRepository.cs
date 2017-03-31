using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuAn.Data.Repositories
{

    public interface IHeSoThoiGianRepository : IRepository<HeSoTg>
    {
    }

    public class HeSoThoiGianRepository : RepositoryBase<HeSoTg>, IHeSoThoiGianRepository
    {
        public HeSoThoiGianRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
