using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface IThongBaoRepository : IRepository<ThongBao>
    {

    }

    public class ThongBaoRepository : RepositoryBase<ThongBao>, IThongBaoRepository
    {
        public ThongBaoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}