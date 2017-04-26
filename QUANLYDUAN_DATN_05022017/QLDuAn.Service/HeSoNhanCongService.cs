using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDuAn.Model.Models;
using QLDuAn.Data.Repositories;
using QLDuAn.Data.Infrastrusture;
using QLDuAn.Common.Exceptions;

namespace QLDuAn.Service
{
    public interface IHeSoNhanCongService
    {
        IEnumerable<HeSoNhanCong> GetAll();

        HeSoNhanCong GetHeSoKcn(int number);

        HeSoNhanCong Add(HeSoNhanCong hsnc);

        HeSoNhanCong GetById(int id);

        void Update(HeSoNhanCong hsnc);

        HeSoNhanCong delete(int id);

        void save();
    }
    public class HeSoNhanCongService : IHeSoNhanCongService
    {
        private IHeSoNhanCongRepository _heSoNhanCongRepository;
        private IUnitOfWork _unitOfWork;

        public HeSoNhanCongService(IHeSoNhanCongRepository heSoNhanCongRepository , IUnitOfWork unitOfWork)
        {
            this._heSoNhanCongRepository = heSoNhanCongRepository;
            this._unitOfWork = unitOfWork;

        }
        public IEnumerable<HeSoNhanCong> GetAll()
        {
            return _heSoNhanCongRepository.GetAll();
        }

        public HeSoNhanCong GetHeSoKcn(int number)
        {
            return _heSoNhanCongRepository.GetByConditon(x=>x.SoNguoiThucHien == number);
        }

        public HeSoNhanCong Add(HeSoNhanCong hsnc)
        {
            var check = _heSoNhanCongRepository.CheckContain(x => x.SoNguoiThucHien.Equals(hsnc.SoNguoiThucHien));
            if (!check)
            {
                return _heSoNhanCongRepository.Add(hsnc);
            }
            else
            {
                throw new NameDuplicateException("Dữ liệu đã tồn tại ~");
            }

        }

        public HeSoNhanCong delete(int id)
        {
            return _heSoNhanCongRepository.Delete(id);
        }

        public HeSoNhanCong GetById(int id)
        {
            return _heSoNhanCongRepository.GetById(id);
        }

        public void save()
        {
            _unitOfWork.Commit();
        }

        public void Update(HeSoNhanCong hsnc)
        {
            _heSoNhanCongRepository.Update(hsnc);
        }
    }
}
