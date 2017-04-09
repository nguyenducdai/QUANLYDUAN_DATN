using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace QLDuAn.Data.Repositories
{
    public interface IDuAnRepository : IRepository<DuAn>
    {
        DuAn GetDetail(int id);

        DuAn GetAllInfo(int id);

        IEnumerable<DuAn> GetDaByIdUser(string idNhanVien); 

    }

    public class DuAnRepository : RepositoryBase<DuAn>, IDuAnRepository
    {
        public DuAnRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public DuAn GetAllInfo(int id)
        {
            var query = from duan in DBContext.DuAn.Include("HopDong").Include("HopDong.KhachHang")
                        select duan;
            return query.SingleOrDefault(x => x.ID.Equals(id));
        }

        public IEnumerable<DuAn> GetDaByIdUser(string idNhanVien)
        {
            var query = from da in DBContext.DuAn
                        join tg in DBContext.ThamGia
                        on da.ID equals tg.IdDuAn
                        where tg.IdNhanVien == idNhanVien
                        select da;
            return query.ToList();
        }

        public DuAn GetDetail(int id)
        {
            var query = from dt in DBContext.DuAn.Include("HopDong")
                        select dt;
            return query.SingleOrDefault(x => x.ID == id);
        }
    }
}