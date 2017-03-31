using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;

namespace QLDuAn.Service
{
    public interface IHeSoThoiGianService
    {
        IEnumerable<HeSoTg> GetAll();
    }

    public class HeSoThoiGianService : IHeSoThoiGianService
    {
        private IHeSoThoiGianRepository _heSoThoiGianRepository;
        private IUnitOfWork _unitOfWork;

        public HeSoThoiGianService(IHeSoThoiGianRepository heSoThoiGianRepository, IUnitOfWork unitOfWork)
        {
            this._heSoThoiGianRepository = heSoThoiGianRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<HeSoTg> GetAll()
        {
            return _heSoThoiGianRepository.GetAll();
        }
    }
}
