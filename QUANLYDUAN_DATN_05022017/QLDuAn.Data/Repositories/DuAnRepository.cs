using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface IDuAnRepository : IRepository<DuAn>
    {

    }

    public class DuAnRepository : RepositoryBase<DuAn>, IDuAnRepository
    {
        public DuAnRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}