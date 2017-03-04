using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface IHangMucRepository : IRepository<HangMuc>
    {

    }

    public class HangMucRepository : RepositoryBase<HangMuc> , IHangMucRepository
    {
        public HangMucRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}