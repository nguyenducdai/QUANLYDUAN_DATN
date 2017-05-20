using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;

namespace QLDuAn.Service
{
    public interface IThamGiaService
    {
        bool Add(IEnumerable<ThamGia> ThamGia, int idHangMuc, int LoaiHangMuc);

        bool delMuti(IEnumerable<ThamGia> ThamGia, int idHangMuc, int LoaiHangMuc);

        void Delete(int id);

        ThamGia GetById(int id);

        IEnumerable<ThamGia> GetAll();

        IEnumerable<ThamGia> GetByIdHm(int IdHangMuc,int LoaiHangMuc);

        IEnumerable<ThamGia> GetListUserByIdHangMuc(int idHangMuc, int loaiHangMuc);

        IEnumerable<ThamGia> GetListUserByIdHangMuc(int idHangMuc);

        IEnumerable<ThamGia> GetListUserByIdHangMuc(int idHangMuc , string idThanhVien);

        decimal TotalPoint(int IdDuAn, int LoaiHangMuc);

        IEnumerable<ThamGia> GetIncomeById(int idDuan, int LoaiHm);

        IEnumerable<ThamGia> GetHangMucByIdUser(int idDuan ,string idNhanVien) ;

        IEnumerable<ThamGia> GetIncomeByIdUser(int IdDuAn, string idNhanVien, int LoaiHangMuc);



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

        public bool Add(IEnumerable<ThamGia> thamGia ,int idHangMuc, int LoaiHangMuc)
        {
            _thamGiaRepository.DeleteMuti(x=>x.IdHangMuc == idHangMuc && x.LoaiHangMuc ==LoaiHangMuc);
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

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ThamGia> GetByIdHm(int IdHangMuc, int LoaiHangMuc)
        {
            return _thamGiaRepository.GetMuti(x => x.IdHangMuc == IdHangMuc && x.LoaiHangMuc == LoaiHangMuc);
        }

        public bool delMuti(IEnumerable<ThamGia> ThamGia, int idHangMuc, int LoaiHangMuc)
        {
            _thamGiaRepository.DeleteMuti(x =>x.IdHangMuc == idHangMuc && x.LoaiHangMuc == LoaiHangMuc);
            return true;
        }

        public IEnumerable<ThamGia> GetListUserByIdHangMuc(int idHangMuc, int loaiHangMuc)
        {
            return _thamGiaRepository.GetMuti(x =>x.IdHangMuc == idHangMuc && x.LoaiHangMuc == loaiHangMuc, new string[] { "ApplicationUser" });
        }

        public IEnumerable<ThamGia> GetListUserByIdHangMuc(int idHangMuc )
        {
            return _thamGiaRepository.GetMuti(x=>x.IdHangMuc.Equals(idHangMuc) , new string[] {"ApplicationUser"});
        }

        public IEnumerable<ThamGia> GetListUserByIdHangMuc(int idHangMuc, string idThanhVien)
        {
            return _thamGiaRepository.GetMuti(x => x.IdHangMuc.Equals(idHangMuc) && x.IdNhanVien.Equals(idThanhVien), new string[] { "ApplicationUser" });
        }

        public decimal TotalPoint(int IdDuAn, int LoaiHangMuc)
        {
            return _thamGiaRepository.TotalPoint(IdDuAn, LoaiHangMuc);
        }

        public IEnumerable<ThamGia> GetIncomeById(int idDuan, int LoaiHm)
        {
            return _thamGiaRepository.GetIncomeById(idDuan, LoaiHm);
        }

        public IEnumerable<ThamGia> GetHangMucByIdUser(int idDuan, string idNhanVien)
        {
            return _thamGiaRepository.GetMuti(x => x.IdDuAn.Equals(idDuan) && x.IdNhanVien.Equals(idNhanVien), new string[] {"HangMuc" , "HangMuc.ApplicationUser" });

        }

        public IEnumerable<ThamGia> GetIncomeByIdUser(int IdDuAn, string idNhanVien, int LoaiHangMuc)
        {
            return _thamGiaRepository.GetIncomeByIdUser(IdDuAn,idNhanVien,LoaiHangMuc);

        }
    }
}