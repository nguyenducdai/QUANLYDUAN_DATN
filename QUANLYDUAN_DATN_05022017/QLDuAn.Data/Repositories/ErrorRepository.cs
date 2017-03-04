using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public class ErrorRepository : RepositoryBase<Errors>
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}