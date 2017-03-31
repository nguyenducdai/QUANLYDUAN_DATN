using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System.Collections.Generic;

namespace QLDuAn.Service
{
    public interface IHeSoLapService
    {
        IEnumerable<HeSoLap> GetAll();
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

        public IEnumerable<HeSoLap> GetAll()
        {
            return _heSoLapRepository.GetAll();
        }
    }
}