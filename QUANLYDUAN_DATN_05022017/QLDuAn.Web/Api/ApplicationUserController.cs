using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using static QLDuAn.Web.App_Start.IdentityConfig;
using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Models;
using System.Threading.Tasks;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Common.Exceptions;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/appuser")]
    public class ApplicationUserController : BaseController
    {
        private ApplicationUserManager _userManager;
        private IApplicationGroupService _applicationGroupService;
        private IApplicationRoleService _applicationRoleService;

        public ApplicationUserController(
            ErrorService errorService , 
            ApplicationUserManager userManager , 
            IApplicationGroupService applicationGroupService,
            IApplicationRoleService applicationRoleService) : base(errorService)
        {
            this._userManager = userManager;
            this._applicationGroupService = applicationGroupService;
            this._applicationRoleService = applicationRoleService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, ()=>{
                HttpResponseMessage response;
                var model = _userManager.Users.OrderByDescending(x => x.Created_at);
                var responseData = Mapper.Map<IEnumerable<ApplicationUser> , IEnumerable<ApplicationUserViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }


        [HttpPost]
        [Route("created")]
        public async Task<HttpResponseMessage> Created(HttpRequestMessage request, ApplicationUserViewModel applicationUserVM)
        {
                if(ModelState.IsValid)
                {
                    var user = new ApplicationUser();
                    user.UpdateApplicationUser(applicationUserVM);
                    try
                    {
                        user.Id = Guid.NewGuid().ToString();
                        var result = await _userManager.CreateAsync(user, applicationUserVM.Password);

                        if (result.Succeeded)
                        {
                            var list = new List<ApplicationUserGroup>();
                            foreach (var group in applicationUserVM.Groups)
                            {
                                list.Add(new ApplicationUserGroup()
                                {
                                    IdGroup = group.Id,
                                    IdUser = user.Id
                                });

                                // thêm quyền cho thành viên dựa vào nhóm người dùng
                                var roles = _applicationRoleService.GetRoleByGroupId(group.Id);
                                var users = _applicationGroupService.GetListUserByGroupId(group.Id);
                                foreach (var u in users)
                                {
                                    foreach (var role in roles)
                                    {
                                         await _userManager.RemoveFromRoleAsync(u.Id , role.Name);
                                         await _userManager.AddToRoleAsync(u.Id , role.Name);
                                    }
                                }
                            }
                            _applicationGroupService.AddUserGroup(list, user.Id);
                            _applicationGroupService.save();
                           return request.CreateResponse(HttpStatusCode.Created, user);
                        }
                        else
                        {
                            return request.CreateResponse(HttpStatusCode.BadRequest, "Thêm thành viên không thành công");

                        }
                    }
                    catch (NameDuplicateException nx)
                    {
                       return request.CreateErrorResponse(HttpStatusCode.BadRequest, nx.Message);
                    }
                    catch (Exception ex)
                    {

                       return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                    }
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
        }

        [HttpGet]
        [Route("detail")]
        public HttpResponseMessage GetDetail(HttpRequestMessage request , string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có dữ liệu ");
            }
            var user = _userManager.FindByIdAsync(id);

            if(user == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "không có dữ liệu");
            }
            else
            {
                var responseData = Mapper.Map<ApplicationUser ,ApplicationUserViewModel>(user.Result);
                var listGroup = _applicationGroupService.GetListGroupByIdUser(responseData.Id);
                responseData.Groups = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
                return request.CreateResponse(HttpStatusCode.OK, responseData);
            }

        }

        [HttpPut]
        [Route("updated")]
        public async Task<HttpResponseMessage> Updated(HttpRequestMessage request, ApplicationUserViewModel applicationUserVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(applicationUserVM.Id);
                    user.UpdateApplicationUser(applicationUserVM);
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var list = new List<ApplicationUserGroup>();
                        foreach (var group in applicationUserVM.Groups)
                        {
                            list.Add(new ApplicationUserGroup()
                            {
                                IdGroup = group.Id,
                                IdUser = user.Id
                            });

                            // thêm quyền cho thành viên dựa vào nhóm người dùng
                            var roles = _applicationRoleService.GetRoleByGroupId(group.Id);
                            var users = _applicationGroupService.GetListUserByGroupId(group.Id);
                            foreach (var u in users)
                            {
                                foreach (var role in roles)
                                {
                                    await _userManager.RemoveFromRoleAsync(u.Id, role.Name);
                                    await _userManager.AddToRoleAsync(u.Id, role.Name);
                                }
                            }
                        }
                        _applicationGroupService.AddUserGroup(list, user.Id);
                        _applicationGroupService.save();
                        return request.CreateResponse(HttpStatusCode.Created, user);
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, "cập nhật không thành công");

                    }
                }
                catch (NameDuplicateException nx)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, nx.Message);
                }
                catch (Exception ex)
                {

                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }



        [HttpDelete]
        [Route("delete")]
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có dữ liệu ");
            }
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return request.CreateResponse(HttpStatusCode.OK,user.UserName);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest,result.Errors);
            }

        }

    }
}
