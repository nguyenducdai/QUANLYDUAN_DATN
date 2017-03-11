using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface INhomCongViecRepository : IRepository<NhomCongViec>
    {
    }

    public class NhomCongViecRepository : RepositoryBase<NhomCongViec>, INhomCongViecRepository
    {
        public NhomCongViecRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}