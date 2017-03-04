using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;

namespace QLDuAn.Service
{
    public interface IHangMucService
    {
        HangMuc Add(HangMuc hangMuc);

        void Update(HangMuc hangMuc);

        void Delete(int id);

        HangMuc getByID(int id);

        IEnumerable<HangMuc> getAll();

        IEnumerable<HangMuc> getPadding(int page, out int total, int pageSize);

        IEnumerable<HangMuc> getAll(string keyword);

        void save();
    }

    public class HangMucService : IHangMucService
    {
        private IHangMucRepository _hangMucRepository;
        private IUnitOfWork _iUnitOfWork;

        public HangMucService(IHangMucRepository hangMucRepository, IUnitOfWork iUnitOfWork)
        {
            this._hangMucRepository = hangMucRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public HangMuc Add(HangMuc hangMuc)
        {
            return _hangMucRepository.Add(hangMuc);
        }

        public void Delete(int id)
        {
            _hangMucRepository.Delete(id);
        }

        public IEnumerable<HangMuc> getAll()
        {
            return _hangMucRepository.GetAll();
        }

        public IEnumerable<HangMuc> getAll(string keyword)
        {
            return _hangMucRepository.GetAll();
        }

        public HangMuc getByID(int id)
        {
           return _hangMucRepository.GetById(id);
        }

        public IEnumerable<HangMuc> getPadding(int page, out int total, int pageSize)
        {
            return _hangMucRepository.GetMutiPaging(x => x.TrangThai, out total, page, pageSize);
        }

        public void save()
        {
            _iUnitOfWork.Commit();
        }

        public void Update(HangMuc hangMuc)
        {
            _hangMucRepository.Update(hangMuc);
        }
    }
}