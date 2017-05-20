using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace QLDuAn.Data.Repositories
{
    public interface IHangMucRepository : IRepository<HangMuc>
    {
        IEnumerable<HangMuc> GetHangMucByIdUser(int idDuAn , string idNhanvien);
    }

    public class HangMucRepository : RepositoryBase<HangMuc> , IHangMucRepository
    {
        public HangMucRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<HangMuc> GetHangMucByIdUser(int idDuAn, string idNhanvien)
        {
            var query = (from hm in DBContext.HangMuc
                        join tg in DBContext.ThamGia
                        on hm.ID equals tg.IdHangMuc
                        where tg.IdDuAn == idDuAn && tg.IdNhanVien == idNhanvien
                        select hm);
            return query;
        }

    }
}