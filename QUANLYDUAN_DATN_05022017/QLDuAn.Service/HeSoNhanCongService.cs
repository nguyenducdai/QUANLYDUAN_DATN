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
    public interface IHeSoNhanCongService
    {
        IEnumerable<HeSoNhanCong> GetAll();

        HeSoNhanCong GetHeSoKcn(int number);
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
    }
}
