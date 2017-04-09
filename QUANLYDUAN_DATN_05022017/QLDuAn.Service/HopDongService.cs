using QLDuAn.Model.Models;
using System.Collections;
using System.Collections.Generic;
using System;
using QLDuAn.Data.Repositories;
using QLDuAn.Data.Infrastrusture;

namespace QLDuAn.Service
{
    public interface IHopDongService
    {
        HopDong Add(HopDong hopDong);

        void Update(HopDong hopDong);

        void Delete(int id);

        IEnumerable<HopDong> GetAll();

        HopDong GetById(int id);

        IEnumerable<HopDong> GetAll(string keyword);

        IEnumerable<HopDong> Paginate(int page , out int total , int pageSize);

        void save();
    }

    public class HopDongService : IHopDongService
    {
        private IHopDongRepository _iHopDongRepository;
        private IUnitOfWork _iUnitOfWork;


        public HopDongService(IHopDongRepository ihopDongRepository , IUnitOfWork iUnitOfWork)
        {
            this._iHopDongRepository = ihopDongRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public HopDong Add(HopDong hopDong)
        {
            return _iHopDongRepository.Add(hopDong);
        }

        public void Delete(int id)
        {
            _iHopDongRepository.Delete(id);
        }

        public IEnumerable<HopDong> GetAll()
        {
            return _iHopDongRepository.GetAll();
        }

        public IEnumerable<HopDong> GetAll(string keyword)
        {
            if(string.IsNullOrEmpty(keyword))
                return _iHopDongRepository.GetAll();
            else
                return _iHopDongRepository.GetMuti(x => x.TenHopDong.Contains(keyword) || x.SoHopDong.Contains(keyword) || x.NoiDung.Contains(keyword));
        }

        public HopDong GetById(int id)
        {
            return _iHopDongRepository.GetById(id);    
        }

        public IEnumerable<HopDong> Paginate(int page, out int total, int pageSize)
        {
            return _iHopDongRepository.GetMutiPaging(x => x.NgayKetThuc < DateTime.Now , out total ,page , pageSize);
        }

        public void Update(HopDong hopDong)
        {
            _iHopDongRepository.Update(hopDong);
        }

        public void save()
        {
            _iUnitOfWork.Commit();
        }
    }
}