using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System.Collections.Generic;
using System;
using QLDuAn.Common.Exceptions;

namespace QLDuAn.Service
{
    public interface IHeSoLapService
    {
        IEnumerable<HeSoLap> GetAll();

        HeSoLap Add(HeSoLap hsl);

        HeSoLap GetById(int id);

        void Update(HeSoLap hsl);

        HeSoLap delete(int id);

        void save();
    }

    public class HeSoLapService : IHeSoLapService
    {
        private IHeSoLapRepository _heSoLapRepository;
        private IUnitOfWork _unitOfWork;

        public HeSoLapService(IHeSoLapRepository heSoLapRepository, IUnitOfWork unitOfWork)
        {
            this._heSoLapRepository = heSoLapRepository;
            this._unitOfWork = unitOfWork;
        }

        public HeSoLap Add(HeSoLap hsl)
        {
            var check = _heSoLapRepository.CheckContain(x => x.SoNam.Equals(hsl.SoNam));
            if (!check)
            {
              return  _heSoLapRepository.Add(hsl);
            }
            else
            {
               throw new NameDuplicateException("Dữ liệu đã tồn tại ~");
            }
           
        }

        public HeSoLap delete(int id)
        {
            return _heSoLapRepository.Delete(id);
        }

        public IEnumerable<HeSoLap> GetAll()
        {
            return _heSoLapRepository.GetAll();
        }

        public HeSoLap GetById(int id)
        {
            return _heSoLapRepository.GetById(id);
        }

        public void save()
        {
            _unitOfWork.Commit();
        }

        public void Update(HeSoLap hsl)
        {
          _heSoLapRepository.Update(hsl);
        }
    }
}