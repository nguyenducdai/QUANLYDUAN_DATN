using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface IThamGiaRepository : IRepository<ThamGia>
    {
    }

    public class ThamGiaRepository : RepositoryBase<ThamGia>, IThamGiaRepository
    {
        public ThamGiaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}