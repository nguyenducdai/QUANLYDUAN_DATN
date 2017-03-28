using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDuAn.Model.Models;
using QLDuAn.Data.Repositories;
using QLDuAn.Data.Infrastrusture;

namespace QLDuAn.Service
{
    public interface IDuAnHangMucService
    {
        DuAnHangMuc Add(DuAnHangMuc duAnHangMuc);

        void Delete(int id);

        DuAnHangMuc GetById(int id);

        void Update(DuAnHangMuc duAnHangMuc);

        IEnumerable<DuAnHangMuc> GetInfoByIdProject(int id , int loaihangmuc);

        bool DeleteMediate(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm);

        DuAnHangMuc GetSingleById(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm);

        DuAnHangMuc GetSingleUpdate(int IdHangMuc, int IdDuAn, int LoaiHm);


        void Save();
}
public class DuAnHangMucService : IDuAnHangMucService
    {
        private IDuAnHangMucRepository _duAnHangMucRepository;
        private IThamGiaRepository _thamGiaRepository;
        private IUnitOfWork _unitOfWork;

        public DuAnHangMucService(IDuAnHangMucRepository duAnHangMucRepository , IThamGiaRepository thamGiaRepository ,IUnitOfWork unitOfWork)
        {
            this._duAnHangMucRepository = duAnHangMucRepository;
            this._thamGiaRepository = thamGiaRepository;
            this._unitOfWork = unitOfWork;
        }

        public DuAnHangMuc Add(DuAnHangMuc duAnHangMuc)
        {
            return _duAnHangMucRepository.Add(duAnHangMuc);
        }

        public void Delete(int id)
        {
            _duAnHangMucRepository.Delete(id);
        }

        public DuAnHangMuc GetById(int id)
        {
           return _duAnHangMucRepository.GetById(id);
        }

        public void Update(DuAnHangMuc duAnHangMuc)
        {
            _duAnHangMucRepository.Update(duAnHangMuc);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<DuAnHangMuc> GetInfoByIdProject(int id , int loaihangmuc)
        {
            return _duAnHangMucRepository.GetMuti(x=>x.IdDuAn.Equals(id) && x.LoaiHangMuc.Equals(loaihangmuc), new string[] {"HangMuc","ApplicationUser" ,"NhomCongViec", "HangMuc.ThamGia", "HangMuc.ThamGia.ApplicationUser" });
        }

        public bool DeleteMediate(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm)
        {
            if (_duAnHangMucRepository.DeleteMediate(IdHangMuc, IdDuAn, IdNhomCV, LoaiHm))
            {
                _thamGiaRepository.DeleteHangMuc( IdDuAn, IdHangMuc, LoaiHm);
                return true;
            }
            else
            {
                return false;
            }
        }

        public DuAnHangMuc GetSingleById(int IdHangMuc, int IdDuAn, int IdNhomCV, int LoaiHm)
        {
            return _duAnHangMucRepository.GetByConditon(x => x.IdDuAn==IdDuAn && x.IdNhomCongViec==IdNhomCV && x.IdHangMuc==IdHangMuc && x.LoaiHangMuc == LoaiHm, new string[] { "HangMuc", "ApplicationUser", "NhomCongViec", "HangMuc.ThamGia", "HangMuc.ThamGia.ApplicationUser" });
        }

        public DuAnHangMuc GetSingleUpdate(int IdHangMuc, int IdDuAn, int LoaiHm)
        {
            return _duAnHangMucRepository.GetByConditon(x => x.IdDuAn == IdDuAn && x.IdHangMuc == IdHangMuc && x.LoaiHangMuc == LoaiHm, new string[] { "HangMuc", "ApplicationUser", "NhomCongViec", "HangMuc.ThamGia", "HangMuc.ThamGia.ApplicationUser" });
        }
    }
}
