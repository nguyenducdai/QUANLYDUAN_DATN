﻿using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;

namespace QLDuAn.Service
{
    public interface IThamGiaService
    {
        ThamGia Add(ThamGia tg);

        void Delete(int id);

        void Update(ThamGia tg);

        ThamGia GetById(int id);

        IEnumerable<ThamGia> GetAll();

        IEnumerable<ThamGia> Paginate(int page, out int total, int pageSize);

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

        public ThamGia Add(ThamGia tg)
        {
            return _thamGiaRepository.Add(tg);
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

        public void Update(ThamGia tg)
        {
            _thamGiaRepository.Update(tg);
        }
    }
}