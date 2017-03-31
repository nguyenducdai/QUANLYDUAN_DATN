using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System.Collections.Generic;
using System;
using QLDuAn.Common.Exceptions;

namespace QLDuAn.Service
{
    public interface IApplicationGroupService
    {
        ApplicationGroup Add(ApplicationGroup applicationGroup);

        void Delete(int id);

        IEnumerable<ApplicationGroup> GetAll();

        IEnumerable<ApplicationGroup> GetAll(int page , out int total , int pageSize, string keyword);

        ApplicationGroup GetDetail(int id);

        void Update(ApplicationGroup applicationGroup);

        bool AddUserGroup(IEnumerable<ApplicationUserGroup> groups , string userId);

        IEnumerable<ApplicationGroup> GetListGroupByIdUser(string idUser);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int idGroup);

        void save();

    }

    public class ApplicationGroupService : IApplicationGroupService
    {
        private IApplicationGroupRepository _applicationGroupRepository;
        private IApplicationUserGroupRepository _applicationUserGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationGroupService(IApplicationGroupRepository applicationGroupRepository, IApplicationUserGroupRepository applicationUserGroupRepository ,IUnitOfWork unitOfWork)
        {
            this._applicationGroupRepository = applicationGroupRepository;
            this._applicationUserGroupRepository = applicationUserGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationGroup Add(ApplicationGroup appGroup)
        {
            if (_applicationGroupRepository.CheckContain(x => x.Name == appGroup.Name))
                throw new NameDuplicateException("lỗi trùng tên nhóm");
             return _applicationGroupRepository.Add(appGroup);
        }

  

        public bool AddUserGroup(IEnumerable<ApplicationUserGroup> userGroups, string userId)
        {
            _applicationUserGroupRepository.DeleteMuti(x=>x.IdUser==userId);
            foreach (var userGroup in userGroups)
            {
                _applicationUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public void Delete(int id)
        {
            _applicationGroupRepository.Delete(id);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _applicationGroupRepository.GetAll();
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, out int total, int pageSize, string keyword)
        {
            return _applicationGroupRepository.GetMutiPaging(x=>x.Name.Contains(keyword) && x.Description.Contains(keyword),out total,page, pageSize);
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _applicationGroupRepository.GetById(id);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByIdUser(string idUser)
        {
           return _applicationGroupRepository.GetListGroupByIdUser(idUser);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int idGroup)
        {
            return _applicationGroupRepository.GetListUserByIdGroup(idGroup);
         
        }

        public void save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup applicationGroup)
        {
            _applicationGroupRepository.Update(applicationGroup);
        }
    }
}