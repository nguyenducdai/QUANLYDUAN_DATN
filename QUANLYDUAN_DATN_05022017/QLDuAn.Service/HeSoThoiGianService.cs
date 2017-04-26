using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using QLDuAn.Common.Exceptions;

namespace QLDuAn.Service
{
    public interface IHeSoThoiGianService
    {
        IEnumerable<HeSoTg> GetAll();

        HeSoTg Add(HeSoTg hsl);

        HeSoTg GetById(int id);

        void Update(HeSoTg hsl);

        HeSoTg delete(int id);

        void save();
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

        public HeSoTg Add(HeSoTg hstg)
        {
            var check = _heSoThoiGianRepository.CheckContain(x => x.ThoiGianDk.Equals(hstg.ThoiGianDk));
            if (!check)
            {
                return _heSoThoiGianRepository.Add(hstg);
            }
            else
            {
                throw new NameDuplicateException("Dữ liệu đã tồn tại ~");
            }

        }

        public HeSoTg delete(int id)
        {
            return _heSoThoiGianRepository.Delete(id);
        }

        public HeSoTg GetById(int id)
        {
            return _heSoThoiGianRepository.GetById(id);
        }

        public void save()
        {
            _unitOfWork.Commit();
        }

        public void Update(HeSoTg hstg)
        {
            _heSoThoiGianRepository.Update(hstg);
        }
    }
}
