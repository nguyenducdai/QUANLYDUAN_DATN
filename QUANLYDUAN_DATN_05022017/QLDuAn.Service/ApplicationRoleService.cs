using QLDuAn.Data.Infrastrusture;
using QLDuAn.Data.Repositories;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;
using QLDuAn.Common.Exceptions;

namespace QLDuAn.Service
{
    public interface IApplicationRoleService
    {
        void Add(ApplicationRole appRole);

        void Update(ApplicationRole appRole);

        void Delete(string id);

        IEnumerable<ApplicationRole> GetAll();

        ApplicationRole GetById(string id);

        void AddRoleGroup(IEnumerable<ApplicationRoleGroup> appRoles, int groupId);

        IEnumerable<ApplicationRole> GetRoleByGroupId(int groupId);

        void Save();
    }

    public class ApplicationRoleService : IApplicationRoleService
    {
        private IApplicationRoleRepository _applicationRoleRepository;
        private IApplicationRoleGroupRepository _applicationRoleGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationRoleService(
            IApplicationRoleRepository applicationRoleRepository ,
            IApplicationRoleGroupRepository applicationRoleGroupRepository,
            IUnitOfWork unitOfWork)
        {
            this._applicationRoleGroupRepository = applicationRoleGroupRepository;
            this._applicationRoleRepository = applicationRoleRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(ApplicationRole appRole)
        {
            if (!_applicationRoleRepository.CheckContain(x => x.Description.Contains(appRole.Description)))
            {
                _applicationRoleRepository.Add(appRole);
            }
            else
            {
                throw new NameDuplicateException("Tên không được trùng");
            }
        }

        public void AddRoleGroup(IEnumerable<ApplicationRoleGroup> appRoles, int groupId)
        {
            _applicationRoleGroupRepository.DeleteMuti(x=>x.GroupId==groupId);
            foreach (var userRole in appRoles)
            {
                _applicationRoleGroupRepository.Add(userRole);
            }
        }

        public void Delete(string id)
        {
            _applicationRoleRepository.DeleteMuti(x=>x.Id==id);
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _applicationRoleRepository.GetAll();
        }

        public ApplicationRole GetById(string id)
        {
            return _applicationRoleRepository.GetByConditon(x=>x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetRoleByGroupId(int groupId)
        {
            return _applicationRoleRepository.GetRoleByGroupId(groupId);
        }

        public void Update(ApplicationRole appRole)
        {
             _applicationRoleRepository.Update(appRole);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}