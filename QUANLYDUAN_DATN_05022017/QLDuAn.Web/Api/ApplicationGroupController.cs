using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using static QLDuAn.Web.App_Start.IdentityConfig;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/applicationgroup")]
    [Authorize]
    public class ApplicationGroupController : BaseController
    {
        private IApplicationGroupService _applicationGroupService;
        private IApplicationRoleService _applicationRoleService;
        private ApplicationUserManager _userManager;

        public ApplicationGroupController(
            ErrorService errorService,
            IApplicationGroupService applicationGroupService,
            IApplicationRoleService applicationRoleService,
            ApplicationUserManager userManager) : base(errorService)
        {
            this._applicationGroupService = applicationGroupService;
            this._applicationRoleService = applicationRoleService;
            this._userManager = userManager;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var ApplicationGroup = _applicationGroupService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(ApplicationGroup);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("created")]
        [HttpPost]
        public HttpResponseMessage Created(HttpRequestMessage request, ApplicationGroupViewModel applicationGroupVM)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newApplicationGroup = new ApplicationGroup();
                    newApplicationGroup.UpdateApplicationGroup(applicationGroupVM);
                    var appGroup = _applicationGroupService.Add(newApplicationGroup);
                    _applicationGroupService.save();

                    var list = new List<ApplicationRoleGroup>();
                    foreach (var role in applicationGroupVM.Roles)
                    {
                        list.Add(new ApplicationRoleGroup()
                        {
                            RoleId = role.Id,
                            GroupId = appGroup.Id
                        });
                    }
                    _applicationRoleService.AddRoleGroup(list, appGroup.Id);
                    _applicationRoleService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, appGroup);
                }
                return response;
            });
        }

        [Route("detail")]
        [HttpGet]
        public HttpResponseMessage Detail(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var applicationGroup = _applicationGroupService.GetDetail(id);
                var responseData = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(applicationGroup);
                var roles = _applicationRoleService.GetRoleByGroupId(id);
                responseData.Roles = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(roles);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("updated")]
        [HttpPut]
        public async Task<HttpResponseMessage> Updated(HttpRequestMessage request, ApplicationGroupViewModel applicationGroupVM)
        {
            HttpResponseMessage response;
            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                try
                {
                    var oldApplicationGroup = _applicationGroupService.GetDetail(applicationGroupVM.Id);
                    oldApplicationGroup.UpdateApplicationGroup(applicationGroupVM);
                    _applicationGroupService.Update(oldApplicationGroup);
                    _applicationGroupService.save();

                    var list = new List<ApplicationRoleGroup>();
                    foreach (var role in applicationGroupVM.Roles)
                    {
                        list.Add(new ApplicationRoleGroup()
                        {
                            RoleId = role.Id,
                            GroupId = applicationGroupVM.Id
                        });
                    }
                    _applicationRoleService.AddRoleGroup(list, applicationGroupVM.Id);
                    _applicationRoleService.Save();

                    var roles = _applicationRoleService.GetRoleByGroupId(applicationGroupVM.Id);
                    var users = _applicationGroupService.GetListUserByGroupId(applicationGroupVM.Id);

                    foreach (var user in users)
                    {
                        foreach (var role in roles)
                        {
                            await _userManager.RemoveFromRoleAsync(user.Id, role.Name);
                            await _userManager.AddToRoleAsync(user.Id, role.Name);
                        }
                    }

                    response = request.CreateResponse(HttpStatusCode.OK, applicationGroupVM);
                }
                catch (Exception ex)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return response;
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                _applicationGroupService.Delete(id);
                _applicationGroupService.save();
                return request.CreateResponse(HttpStatusCode.OK, id);
            });
        }
    }
}