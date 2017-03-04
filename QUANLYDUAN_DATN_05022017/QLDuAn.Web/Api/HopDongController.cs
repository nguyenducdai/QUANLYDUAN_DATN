﻿using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/hd")]
    public class HopDongController : BaseController
    {
        #region
        private IHopDongService _iHopDongService;
        private IKhachHangService _ikhService ;

        public HopDongController(
            ErrorService errorService,
            IHopDongService iHopDongService,
            IKhachHangService ikhService
        ) : base(errorService)
        {
            this._iHopDongService = iHopDongService;
            this._ikhService = ikhService;
        }
        #endregion

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateReponse(request, () =>
            {
                var model = _iHopDongService.GetAll();
                var responseData = Mapper.Map<IEnumerable<HopDong>, IEnumerable<HopDongViewModel>>(model);
                HttpResponseMessage response = request.CreateResponse(System.Net.HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage created(HttpRequestMessage request, HopDongViewModel hopDongViewModel)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var hopDong = new HopDong();
                    hopDong.UpdateHopDong(hopDongViewModel);

                    var model = _iHopDongService.Add(hopDong);
                    _iHopDongService.save();
                    var responseData = Mapper.Map<HopDong, HopDongViewModel>(model);
                    response = request.CreateResponse(System.Net.HttpStatusCode.Created, responseData);
                }
                else
                {
                    response = request.CreateResponse(System.Net.HttpStatusCode.BadGateway, ModelState.IsValid);
                }

                return response;

            });
        }


        [HttpGet]
        [Route("getbyid")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateReponse(request, () =>
            {
                var model = _iHopDongService.GetById(id);
                var responseData = Mapper.Map<HopDong, HopDongViewModel>(model);
                HttpResponseMessage respose = request.CreateResponse(HttpStatusCode.OK, responseData);
                return respose;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, HopDongViewModel hopDongViewModel)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var hopDong = _iHopDongService.GetById(hopDongViewModel.ID);
                    hopDong.UpdateHopDong(hopDongViewModel);
                    _iHopDongService.Update(hopDong);
                    _iHopDongService.save();
                    var responseData = Mapper.Map<HopDong, HopDongViewModel>(hopDong);
                    response = request.CreateResponse(System.Net.HttpStatusCode.Created, responseData);
                }
                else
                {
                    response = request.CreateResponse(System.Net.HttpStatusCode.BadGateway, ModelState.IsValid);
                }

                return response;

            });
        }
        
        [HttpGet]
        [Route("getcustomer")]
        public HttpResponseMessage GetCustomer(HttpRequestMessage request)
        {
            return CreateReponse(request , ()=> {
              var model = _ikhService.GetAll();
                var resposeData = Mapper.Map<IEnumerable<KhachHang> , IEnumerable<KhachHangViewModel>>(model);
                HttpResponseMessage repose = request.CreateResponse(HttpStatusCode.OK ,resposeData);
                return repose;
            });
        }


    }
}