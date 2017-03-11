using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuAn.Service
{
    public interface INhomCongViecService
    {
        NhomCongViec Add(NhomCongViec cv);

        void Delete(int id);

        void Update(NhomCongViec cv);

        NhomCongViec GetById(int id);

        IEnumerable<NhomCongViec> GetAll();

        IEnumerable<NhomCongViec> Paginate(int page, out int total, int pageSize);

        void Save();
    }
    public class NhomCongViecService : INhomCongViecService
    {
        private INhomCongViecRepository _nhomCongViecRepostitory;
        private IUnitOfWork _unitOfWork;

        public NhomCongViecService(INhomCongViecRepository nhomCongViecRepostitory, IUnitOfWork unitOfWork)
        {
            this._nhomCongViecRepostitory = nhomCongViecRepostitory;
            this._unitOfWork = unitOfWork;
        }
        public NhomCongViec Add(NhomCongViec cv)
        {
            return _nhomCongViecRepostitory.Add(cv);
        }

        public void Delete(int id)
        {
            _nhomCongViecRepostitory.Delete(id);
        }

        public IEnumerable<NhomCongViec> GetAll()
        {
            return _nhomCongViecRepostitory.GetAll();
        }

        public NhomCongViec GetById(int id)
        {
            return _nhomCongViecRepostitory.GetById(id);
        }

        public IEnumerable<NhomCongViec> Paginate(int page, out int total, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(NhomCongViec cv)
        {
            _nhomCongViecRepostitory.Update(cv);
        }
    }
}
