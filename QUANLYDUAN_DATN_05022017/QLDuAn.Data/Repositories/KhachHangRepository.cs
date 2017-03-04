using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;

namespace QLDuAn.Data.Repositories
{
    public interface IKhachHangRepository : IRepository<KhachHang>
    {

    }

    public class KhachHangRepository : RepositoryBase<KhachHang>, IKhachHangRepository
    {
        public KhachHangRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}