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
            var query = from tg in DBContext.ThamGia
                        where tg.IdDuAn == IdDuAn && tg.LoaiHangMuc == LoaiHangMuc
                        select tg;
            return query.Sum(x=>x.DiemThanhVien);
        }
    }
}