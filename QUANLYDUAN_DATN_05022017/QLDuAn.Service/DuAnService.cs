using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;

namespace QLDuAn.Service
{
    public interface IDuAnService
    {
        DuAn Add(DuAn duAn);

        void Update(DuAn duAn);

        void Delete(int id);

        DuAn GetById(int id);

        IEnumerable<DuAn> GetAll();

        IEnumerable<DuAn> Paginate(int page, out int total, int pageSize);

        IEnumerable<DuAn> GetAll(string keyword);

        DuAn GetDetail(int id);

        DuAn GetAllInfoById(int id);

        IEnumerable<DuAn> GetDaByIdUser(string idNhanVien);

        void Save();
    }

    public class DuAnService : IDuAnService
    {
        private IDuAnRepository _duAnRepository;
        private IUnitOfWork _iUnitOfWork;

        public DuAnService(IDuAnRepository duAnRepository, IUnitOfWork iUnitOfWork)
        {
            this._duAnRepository = duAnRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public DuAn Add(DuAn duAn)
        {
            return _duAnRepository.Add(duAn);
        }

        public void Delete(int id)
        {
            _duAnRepository.Delete(id);
        }

        public IEnumerable<DuAn> GetAll()
        {
            return _duAnRepository.GetAll();
        }

        public IEnumerable<DuAn> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return _duAnRepository.GetAll(new string[] {"KhachHang","HangMuc"});
            else
                return _duAnRepository.GetMuti(x => x.TenDuAn.Contains(keyword) && x.MoTa.Contains(keyword), new string[] {"KhachHang"});
        }
        public DuAn GetById(int id)
        {
            return _duAnRepository.GetByConditon(x=>x.ID.Equals(id), new string[] {"HangMuc" });
        }

        public IEnumerable<DuAn> Paginate(int page, out int total, int pageSize)
        {
            return _duAnRepository.GetMutiPaging(x => x.TrangThai.Equals(1), out total, page, pageSize);
        }

        public void Update(DuAn duAn)
        {
            _duAnRepository.Update(duAn);
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

        public DuAn GetDetail(int id)
        {
            return _duAnRepository.GetByConditon(x => x.ID== id, new string[] { "KhachHang" });
        }

        public DuAn GetAllInfoById(int id)
        {
            return _duAnRepository.GetAllInfo(id);
        }

        public IEnumerable<DuAn> GetDaByIdUser(string idNhanVien)
        {
            return _duAnRepository.GetDaByIdUser(idNhanVien);
        }
    }
}