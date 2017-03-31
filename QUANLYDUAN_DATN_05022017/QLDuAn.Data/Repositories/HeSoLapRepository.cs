using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface IHeSoLapRepository : IRepository<HeSoLap>
    {
    }

    public class HeSoLapRepository : RepositoryBase<HeSoLap>, IHeSoLapRepository
    {
        public HeSoLapRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}