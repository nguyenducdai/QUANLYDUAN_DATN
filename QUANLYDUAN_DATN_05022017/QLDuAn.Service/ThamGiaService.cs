using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;

namespace QLDuAn.Service
{
    public interface IThamGiaService
    {
        bool Add(IEnumerable<ThamGia> ThamGia , int IdDuAn, int idHangMuc  , int LoaiHangMuc);

        bool delMuti(IEnumerable<ThamGia> ThamGia , int IdDuAn, int idHangMuc, int LoaiHangMuc);

        void Delete(int id);

        ThamGia GetById(int id);

        IEnumerable<ThamGia> GetAll();

        IEnumerable<ThamGia> Paginate(int page, out int total, int pageSize);

        IEnumerable<ThamGia> GetByIdHm(int IdHangMuc, int IdDuAn, int LoaiHangMuc);

        void Save();
    }

    public class ThamGiaService : IThamGiaService
    {
        private IThamGiaRepository _thamGiaRepository;
        private IUnitOfWork _unitOfWork;

        public ThamGiaService(IThamGiaRepository thamGiaRepository, IUnitOfWork unitOfWork)
        {
            this._thamGiaRepository = thamGiaRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool Add(IEnumerable<ThamGia> thamGia, int IdDuAn, int idHangMuc, int LoaiHangMuc)
        {
            _thamGiaRepository.DeleteMuti(x => x.IdDuAn == IdDuAn && x.IdHangMuc == idHangMuc && x.LoaiHangMuc == LoaiHangMuc);
            foreach (var item in thamGia)
            {
                _thamGiaRepository.Add(item);
            }
            return true;
        }

        public void Delete(int id)
        {
            _thamGiaRepository.Delete(id);
        }

        public IEnumerable<ThamGia> GetAll()
        {
            return _thamGiaRepository.GetAll();
        }

        public ThamGia GetById(int id)
        {
            return _thamGiaRepository.GetById(id);
        }

        public IEnumerable<ThamGia> Paginate(int page, out int total, int pageSize)
        {
            // return _thamGiaRepository.GetMutiPaging(x => x.IdDuAn, out total,page , pageSize);
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ThamGia> GetByIdHm(int IdHangMuc, int IdDuAn, int LoaiHangMuc)
        {
            return _thamGiaRepository.GetMuti(x=>x.IdHangMuc==IdHangMuc && x.IdDuAn==IdDuAn && x.LoaiHangMuc==LoaiHangMuc);
        }

        public bool delMuti(IEnumerable<ThamGia> ThamGia, int IdDuAn, int idHangMuc, int LoaiHangMuc)
        {
            _thamGiaRepository.DeleteMuti(x => x.IdDuAn == IdDuAn && x.IdHangMuc == idHangMuc && x.LoaiHangMuc == LoaiHangMuc);
            return true;
        }
    }
}