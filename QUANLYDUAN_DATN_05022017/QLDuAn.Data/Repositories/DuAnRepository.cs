using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
using System.Linq;

namespace QLDuAn.Data.Repositories
{
    public interface IDuAnRepository : IRepository<DuAn>
    {
        DuAn GetDetail(int id);
    }

    public class DuAnRepository : RepositoryBase<DuAn>, IDuAnRepository
    {
        public DuAnRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public DuAn GetDetail(int id)
        {
            var query = from dt in DBContext.DuAn.Include("HopDong")
                        select dt;
            return query.SingleOrDefault(x => x.ID == id);
        }

        //public DuAn GetDetail1(int id)
        //{
        //    var query = (from sp in DBContext.DuAn
        //                 join dm in DBContext.HopDong
        //                 on sp.ID equals dm.ID
        //                 where sp.ID == id
        //                 select new DuAn()
        //                 {
        //                     sp.HopDong = dm.DuAn 

                          

        //                 });
        //    return query;

        //}
    }
}