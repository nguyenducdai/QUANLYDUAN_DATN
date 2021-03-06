﻿using QLDuAn.Common.Exceptions;
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

        IEnumerable<HangMuc> GetHangMucDuAn(int idDuAn, int LoaiHm, string keyword , bool? filter);

        IEnumerable<HangMuc> getAll(string keyword);

        HangMuc GetHangMucById(int idHangMuc);

        IEnumerable<HangMuc> GetHangMucByIdDuAn(int idDuAn);

        IEnumerable<HangMuc> GetHangMucByIdDuAn(int idDuAn , int LoaiHm);

        IEnumerable<HangMuc> GetHangMucByIdDuAnSuccess(int idDuAn);

        IEnumerable<HangMuc> GetHangMucByIdUser(int idDuAn, string idNhanvien);

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
            var check = _hangMucRepository.CheckContain(x => x.TenHangMuc.Equals(hangMuc.TenHangMuc));
            if (!check)
                return _hangMucRepository.Add(hangMuc);
            else
                throw new NameDuplicateException("Hạng mục công việc đã tồn tại");
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
             if(string.IsNullOrEmpty(keyword))
                return _hangMucRepository.GetAll();
              else
                return _hangMucRepository.GetMuti(x => x.TenHangMuc.Contains(keyword) || x.MoTaHangMuc.Contains(keyword));
        }

        public HangMuc getByID(int id)
        {
           return _hangMucRepository.GetById(id);
        }

        public HangMuc GetHangMucById(int idHangMuc)
        {
            return _hangMucRepository.GetByConditon(x => x.ID.Equals(idHangMuc), new string[] { "ApplicationUser", "ThamGia", "NhomCongViec", "HeSoTg", "HeSoLap", "ThamGia.ApplicationUser" });
        }

        public IEnumerable<HangMuc> GetHangMucByIdDuAn(int idDuAn, int LoaiHm)
        {
            return _hangMucRepository.GetMuti(x => x.IdDuAn.Equals(idDuAn) && x.LoaiHangMuc.Equals(LoaiHm) && x.isDelete.Equals(false), new string[] { "ApplicationUser", "ThamGia", "NhomCongViec", "HeSoTg", "HeSoLap", "ThamGia.ApplicationUser" });

        }

        public IEnumerable<HangMuc> GetHangMucByIdDuAnSuccess(int idDuAn)
        {
            return _hangMucRepository.GetMuti(x => x.IdDuAn.Equals(idDuAn) && x.TrangThai==true);
        }

        public IEnumerable<HangMuc> GetHangMucDuAn(int idDuAn, int LoaiHm, string keyword , bool? filter)
        {
             if (!string.IsNullOrEmpty(keyword) && filter ==true)
            {
                return _hangMucRepository.GetMuti(x =>x.TrangThai==true && x.isDelete==false && x.IdDuAn.Equals(idDuAn) && x.LoaiHangMuc.Equals(LoaiHm) && x.TenHangMuc.Contains(keyword) || x.MoTaHangMuc.Contains(keyword), new string[] { "ApplicationUser", "ThamGia", "NhomCongViec", "HeSoTg", "HeSoLap", "ThamGia.ApplicationUser" });
            }
            else if (!string.IsNullOrEmpty(keyword) && filter == false )
            {
                return _hangMucRepository.GetMuti(x => x.TrangThai == false && x.isDelete==false && x.IdDuAn.Equals(idDuAn) && x.LoaiHangMuc.Equals(LoaiHm) && x.TenHangMuc.Contains(keyword) || x.MoTaHangMuc.Contains(keyword), new string[] { "ApplicationUser", "ThamGia", "NhomCongViec", "HeSoTg", "HeSoLap", "ThamGia.ApplicationUser" });
            }
             else if (string.IsNullOrEmpty(keyword) && filter == true)
            {
                return _hangMucRepository.GetMuti(x =>x.TrangThai==true && x.isDelete == false && x.IdDuAn.Equals(idDuAn) && x.LoaiHangMuc.Equals(LoaiHm), new string[] { "ApplicationUser", "ThamGia", "NhomCongViec", "HeSoTg", "HeSoLap", "ThamGia.ApplicationUser" });
            }
            else if (string.IsNullOrEmpty(keyword) && filter == false)
            {
                return _hangMucRepository.GetMuti(x => x.TrangThai == false && x.isDelete == false && x.IdDuAn.Equals(idDuAn) && x.LoaiHangMuc.Equals(LoaiHm), new string[] { "ApplicationUser", "ThamGia", "NhomCongViec", "HeSoTg", "HeSoLap", "ThamGia.ApplicationUser" });
            }
            if (!string.IsNullOrEmpty(keyword) && filter == null)
            {
                return _hangMucRepository.GetMuti(x => x.IdDuAn.Equals(idDuAn) && x.isDelete == false  && x.LoaiHangMuc.Equals(LoaiHm) && x.TenHangMuc.Contains(keyword) || x.MoTaHangMuc.Contains(keyword), new string[] { "ApplicationUser", "ThamGia", "NhomCongViec", "HeSoTg", "HeSoLap", "ThamGia.ApplicationUser" });
            }
            else
            {
                return _hangMucRepository.GetMuti(x => x.IdDuAn.Equals(idDuAn) && x.isDelete == false  && x.LoaiHangMuc.Equals(LoaiHm), new string[] { "ApplicationUser", "ThamGia", "NhomCongViec", "HeSoTg", "HeSoLap", "ThamGia.ApplicationUser" });
            }
        }

        
        public void save()
        {
            _iUnitOfWork.Commit();
        }

        public void Update(HangMuc hangMuc)
        {
            var check = _hangMucRepository.CheckContain(x => x.TenHangMuc.Equals(hangMuc.TenHangMuc) && x.ID != hangMuc.ID);
            if (!check)
                _hangMucRepository.Update(hangMuc);
            else
                throw new NameDuplicateException("Tên hạng mục đã tồn tại");
        }

       public IEnumerable<HangMuc> GetHangMucByIdDuAn(int idDuAn)
        {
            return _hangMucRepository.GetMuti(x => x.IdDuAn.Equals(idDuAn));
        }

        public IEnumerable<HangMuc> GetHangMucByIdUser(int idDuAn, string idNhanvien)
        {
            return _hangMucRepository.GetHangMucByIdUser(idDuAn,idNhanvien);
        }

    }
}