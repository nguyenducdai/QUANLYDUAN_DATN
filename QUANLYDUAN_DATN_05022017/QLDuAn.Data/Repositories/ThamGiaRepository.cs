using System;
using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
using System.Linq;
using System.Collections.Generic;

namespace QLDuAn.Data.Repositories
{
    public interface IThamGiaRepository : IRepository<ThamGia>
    {
        void DeleteHangMuc(int IdDuAn, int IdHangMuc, int LoaiHangMuc);

        decimal TotalPoint(int IdDuAn,int LoaiHangMuc);

        IEnumerable<ThamGia> GetIncomeById(int idDuan, int LoaiHm);

    }

    public class ThamGiaRepository : RepositoryBase<ThamGia>, IThamGiaRepository
    {
        public ThamGiaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public void DeleteHangMuc(int IdDuAn, int IdHangMuc, int LoaiHangMuc)
        {
            List<ThamGia> thamgias =
               (from thamgia in DBContext.ThamGia
                where thamgia.IdHangMuc == IdHangMuc && thamgia.LoaiHangMuc == LoaiHangMuc
                select thamgia).ToList();

            if (thamgias.Count > 0)
            {
                foreach (var item in thamgias)
                {
                    DBContext.ThamGia.Remove(item);
                    DBContext.SaveChanges();
                }
            }
        }

        public decimal TotalPoint(int IdDuAn, int LoaiHangMuc)
        {
            decimal d = 0;
            var query = from tg in DBContext.ThamGia
                        where tg.IdDuAn == IdDuAn && tg.LoaiHangMuc == LoaiHangMuc
                        select tg;
            if(query.Count() > 0)
            {
                return query.Sum(x => x.DiemThanhVien);
            }
            return d;
        }

        public IEnumerable<ThamGia> GetIncomeById(int idDuan, int loaiHm)
        {
            var query = (from tg in DBContext.ThamGia.Include("ApplicationUser")
                         join u in DBContext.Users
                         on tg.IdNhanVien equals u.Id
                         where tg.LoaiHangMuc == loaiHm && tg.IdDuAn == idDuan
                         group tg by
                         new
                         {
                             tg.IdNhanVien,
                             tg.ApplicationUser

                         }
                         into t
                         select new 
                         {
                             DiemThanhVien = t.Sum(x => x.DiemThanhVien),
                             ThuNhap = t.Sum(x => x.ThuNhap),
                             IdNhanVien = t.Key.IdNhanVien,
                            ApplicationUser = t.Key.ApplicationUser
                             
                         })
                         .AsEnumerable().Select(x => new ThamGia() {

                             DiemThanhVien = x.DiemThanhVien,
                             ThuNhap = x.ThuNhap,
                             IdNhanVien = x.IdNhanVien,
                             ApplicationUser = x.ApplicationUser
                        });
            return query.ToList();
        }

    }
}