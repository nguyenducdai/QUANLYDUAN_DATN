using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using System.Linq;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/hm")]
    [Authorize]
    public class HangMucController : BaseController
    {
        #region
        private HangMucService _hangMucService;

        public HangMucController(HangMucService hangMucService, ErrorService error) : base(error)
        {
            this._hangMucService = hangMucService;
        }

        #endregion
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage respose = null;
                var model = _hangMucService.getAll();
                var responseData = Mapper.Map<IEnumerable<HangMuc>, IEnumerable<HangMucViewModel>>(model);
                respose = request.CreateResponse(HttpStatusCode.OK, model);
                return respose;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                var model = _hangMucService.getByID(id);
                var responseData = Mapper.Map<HangMuc, HangMucViewModel>(model);
                HttpResponseMessage respose = request.CreateResponse(HttpStatusCode.OK, responseData);
                return respose;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Created(HttpRequestMessage request, HangMucViewModel hangMucViewModel)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage respose;
                if (!ModelState.IsValid)
                {
                    respose = request.CreateResponse(HttpStatusCode.BadRequest, ModelState.IsValid);
                }else
                {
                    var hangMuc = new HangMuc();
                    hangMuc.UpdateHangMuc(hangMucViewModel);
                    var model = _hangMucService.Add(hangMuc);
                    _hangMucService.save();
                    var reposeData = Mapper.Map<HangMuc, HangMucViewModel>(model);
                    respose = request.CreateResponse(HttpStatusCode.Created, reposeData);
                }
                
                return respose;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, HangMucViewModel hangMucViewModel)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage respose = null;
                if (ModelState.IsValid)
                {
                    var hangmucmoi = _hangMucService.getByID(hangMucViewModel.ID);
                    hangmucmoi.UpdateHangMuc(hangMucViewModel);
                    _hangMucService.Update(hangmucmoi);
                    _hangMucService.save();
                    respose = request.CreateResponse(HttpStatusCode.Accepted, hangMucViewModel.ID);
                }

                return respose;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                _hangMucService.Delete(id);
                _hangMucService.save();
                HttpResponseMessage respose = request.CreateResponse(HttpStatusCode.OK, id); ;
                return respose;
            });
        }


        [HttpGet]
        [Route("getbylhm")]
        public HttpResponseMessage GetByLHM(HttpRequestMessage request ,int option)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var query = _hangMucService.getAll();
                if (option.Equals(0))
                {
                    query = query.Where(x=>x.LoaiHangMuc);

                }else
                {
                    query = query.Where(x => x.LoaiHangMuc.Equals(false));
                }

                var reposeData = Mapper.Map<IEnumerable<HangMuc>, IEnumerable<HangMucViewModel>>(query);

                response = request.CreateResponse(HttpStatusCode.OK,reposeData);
                return response;


            });

        }
    }
}