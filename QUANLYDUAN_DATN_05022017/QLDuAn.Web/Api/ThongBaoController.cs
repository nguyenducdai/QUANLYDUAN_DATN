using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Models;
using QLDuAn.Model.Models;
using QLDuAn.Web.Infastructure.Extentions;
using AutoMapper;



namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/tb")]
    public class ThongBaoController : BaseController
    {
        private IThongBaoService _thongBaoService;
        public ThongBaoController(ErrorService errorService, IThongBaoService thongBaoService) : base(errorService)
        {
            this._thongBaoService = thongBaoService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage Updated(HttpRequestMessage resquest , int page , int pageSize , string keyword)
        {
            return CreateReponse(resquest, () => {
                var model = _thongBaoService.GetAll(keyword);
                var query = model.OrderByDescending(x => x.Created_at).Skip(page * pageSize).Take(pageSize);
                var responData = Mapper.Map<IEnumerable<ThongBao>, IEnumerable<ThongBaoViewModel>>(query);

                Paginnation<ThongBaoViewModel> pagination = new Paginnation<ThongBaoViewModel> {
                    Page = page,
                    items=responData,
                    TotalPage = Convert.ToInt32(Math.Ceiling((decimal)model.Count()/pageSize)),
                    TotalCount = model.Count()
                };
                return resquest.CreateResponse(HttpStatusCode.Created, pagination);
            });
        }


        [HttpGet]
        [Route("getbyid")]
        public HttpResponseMessage Updated(HttpRequestMessage resquest, int id)
        {
            return CreateReponse(resquest, () => {

                if (!ModelState.IsValid)
                {
                    return resquest.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                }
                else
                {
                    var thongbao = _thongBaoService.GetById(id); ;
                    var responData = Mapper.Map<ThongBao , ThongBaoViewModel>(thongbao);
                    return resquest.CreateResponse(HttpStatusCode.Created, responData);
                }
            });
        }

        [HttpPost]
        [Route("created")]
        public HttpResponseMessage Created(HttpRequestMessage resquest , ThongBaoViewModel thongBaoVM)
        {
            return CreateReponse(resquest,()=> {

                if (!ModelState.IsValid)
                {
                    return resquest.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                }
                else
                {
                    var thongbao = new ThongBao();
                    thongbao.UpdateThongBao(thongBaoVM);
                    thongbao.NguoiTao = User.Identity.Name;
                    thongbao.Created_at = DateTime.Now;
                    var responData = _thongBaoService.Add(thongbao);
                    _thongBaoService.save();
                    return resquest.CreateResponse(HttpStatusCode.Created, thongBaoVM);
                }
            });
        }


        [HttpPut]
        [Route("updated")]
        public HttpResponseMessage Updated(HttpRequestMessage resquest, ThongBaoViewModel thongBaoVM)
        {
            return CreateReponse(resquest, () => {

                if (!ModelState.IsValid)
                {
                    return resquest.CreateResponse(HttpStatusCode.BadRequest, ModelState);

                }
                else
                {
                    var thongbao = _thongBaoService.GetById(thongBaoVM.Id); ;
                    thongbao.UpdateThongBao(thongBaoVM);
                    thongbao.NguoiTao = User.Identity.Name;
                    _thongBaoService.Update(thongbao);
                    _thongBaoService.save();
                    return resquest.CreateResponse(HttpStatusCode.Created, thongBaoVM);
                }
            });
        }

        [HttpDelete]
        [Route("deleted")]
        public HttpResponseMessage Deleted(HttpRequestMessage resquest, int id)
        {
            return CreateReponse(resquest, () => {
                _thongBaoService.Del(id);
                _thongBaoService.save();
                return resquest.CreateResponse(HttpStatusCode.OK, id);
            });
        }

    }
}
