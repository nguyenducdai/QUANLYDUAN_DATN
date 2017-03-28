using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
using System.Linq;
using System;

namespace QLDuAn.Data.Repositories
{
    public interface IDuAnHangMucRepository : IRepository<DuAnHangMuc>
    {
        bool DeleteMediate(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm);

        DuAnHangMuc GetSingle(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm);
      
    }

    public class DuAnHangMucRepository : RepositoryBase<DuAnHangMuc>, IDuAnHangMucRepository
    {
        public DuAnHangMucRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        private DuAnHangMuc Query(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm)
        {

            var query = (from duanHm in DBContext.DuAnHangMuc
                         where duanHm.IdHangMuc == IdHangMuc && duanHm.IdDuAn == IdDuAn && duanHm.IdNhomCongViec == IdNhomCV &&
                         duanHm.LoaiHangMuc == LoaiHm
                         select duanHm).FirstOrDefault();
            return query;
        }

        public bool DeleteMediate(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm)
        {
            var query = this.Query(IdHangMuc,IdDuAn, IdNhomCV, LoaiHm);
            if(query != null)
            {
                DBContext.DuAnHangMuc.Remove(query);
                DBContext.SaveChanges();
                return true;
            }
            return false;
        }

        public DuAnHangMuc GetSingle(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm)
        {
            return this.Query(IdHangMuc, IdDuAn, IdNhomCV, LoaiHm);
        }

    }
}