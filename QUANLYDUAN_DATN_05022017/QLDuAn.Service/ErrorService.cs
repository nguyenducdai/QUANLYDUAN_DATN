using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;

namespace QLDuAn.Service
{
    internal interface IErrorService
    {
        Errors Add(Errors error);
    }

    public class ErrorService : IErrorService
    {
        private ErrorRepository _errorRepository;

        public ErrorService(ErrorRepository errorRepository)
        {
            this._errorRepository = errorRepository;
            // this._iUnitOfWork = iUnitOfWork;
        }

        public Errors Add(Errors error)
        {
            return _errorRepository.Add(error);
        }
    }
}