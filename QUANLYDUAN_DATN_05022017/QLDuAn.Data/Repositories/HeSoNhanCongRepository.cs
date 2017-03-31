using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface IHeSoNhanCongRepository : IRepository<HeSoNhanCong>
    {
    }

    public class HeSoNhanCongRepository : RepositoryBase<HeSoNhanCong>, IHeSoNhanCongRepository
    {
        public HeSoNhanCongRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}