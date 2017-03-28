using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;

namespace QLDuAn.Service
{
    public interface IKhachHangService
    {
        KhachHang Add(KhachHang khachHang);

        void Delete(int id);

        void Update(KhachHang khachHang);

        KhachHang GetById(int id);

        IEnumerable<KhachHang> GetAll();

        IEnumerable<KhachHang> GetAll(string keyword);

        IEnumerable<KhachHang> Paginate(int page, out int total, int pageSize);

        void save();
    }

    public class KhachHangService : IKhachHangService
    {
        private IKhachHangRepository _iKhacHangRepository;
        private IUnitOfWork _iUnitOfWork;

        #region
        public KhachHangService(IKhachHangRepository iKhacHangRepository, IUnitOfWork iUnitOfWork)
        {
            this._iKhacHangRepository = iKhacHangRepository;
            this._iUnitOfWork = iUnitOfWork;
        }
        #endregion

        public KhachHang Add(KhachHang khachHang)
        {
            return _iKhacHangRepository.Add(khachHang);
        }

        public void Delete(int id)
        {
            _iKhacHangRepository.Delete(id);
        }

        public IEnumerable<KhachHang> GetAll()
        {
            return _iKhacHangRepository.GetAll();
        }

        public IEnumerable<KhachHang> GetAll(string keyword)
        {
            return _iKhacHangRepository.GetMuti(x => x.TenKhach.Contains(keyword));
        }

        public KhachHang GetById(int id)
        {
            return _iKhacHangRepository.GetById(id);
        }

        public IEnumerable<KhachHang> Paginate(int page, out int total, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Update(KhachHang khachHang)
        {
            _iKhacHangRepository.Update(khachHang);
        }

        public void save()
        {
            _iUnitOfWork.Commit();
        }
    }
}