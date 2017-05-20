using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QLDuAn.Service
{
    public interface IThongBaoService
    {
        ThongBao Add(ThongBao tb);

        void Del(int id);

        IEnumerable<ThongBao> GetAll();

        IEnumerable<ThongBao> GetAlertByDateNow();

        IEnumerable<ThongBao> GetAll(string keyword);

        IEnumerable<ThongBao> GetListPaging(int page , int pageSize , out int total);

        ThongBao GetById(int id);

        void Update(ThongBao tb);

        void save();

    }
    public class ThongBaoService : IThongBaoService
    {
        private IThongBaoRepository _thongBaoRepository;
        private IUnitOfWork _unitOfWork;
        public ThongBaoService(IThongBaoRepository thongBaoRepository, IUnitOfWork unitOfWork)
        {
            this._thongBaoRepository = thongBaoRepository;
            this._unitOfWork = unitOfWork;
        }

        public ThongBao Add(ThongBao tb)
        {
            return _thongBaoRepository.Add(tb);
        }

        public void Del(int id)
        {
            _thongBaoRepository.Delete(id);
        }

        public IEnumerable<ThongBao> GetAlertByDateNow()
        {
            return _thongBaoRepository.GetMuti(x=>x.Created_at.Value.Day ==  DateTime.Now.Day && x.Created_at.Value.Month == DateTime.Now.Month && x.Created_at.Value.Year == DateTime.Now.Year);
        }

        public IEnumerable<ThongBao> GetAll()
        {
            return _thongBaoRepository.GetAll();
        }

        public IEnumerable<ThongBao> GetAll(string keyword)
        {
            if(!string.IsNullOrEmpty(keyword))
                return _thongBaoRepository.GetMuti(x=>x.TieuDe.Contains(keyword) || x.NoiDung.Contains(keyword));
            else
                return _thongBaoRepository.GetAll();
        }

        public ThongBao GetById(int id)
        {
            return _thongBaoRepository.GetById(id);
        }

        public IEnumerable<ThongBao> GetListPaging(int page, int pageSize, out int total)
        {
            var model = _thongBaoRepository.GetMuti(x=>x.TieuDe != null);
            total = model.Count();
            return model.OrderByDescending(x => x.Created_at).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ThongBao tb)
        {
            _thongBaoRepository.Update(tb);
        }
    }
}
