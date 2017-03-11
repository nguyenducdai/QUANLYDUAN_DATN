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

        IEnumerable<DuAn> GetAll(string keyWord);

        DuAn GetDetail(int id);

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

        public IEnumerable<DuAn> GetAll(string keyWord)
        {
            return _duAnRepository.GetMuti(x => x.TenDuAn.Contains(keyWord) && x.MoTa.Contains(keyWord));
        }

        public DuAn GetById(int id)
        {
            return _duAnRepository.GetById(id);
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
            return _duAnRepository.GetDetail(id);
        }
    }
}