﻿using System.Web.Http;
using QLDuAn.Web.Infastructure.Core;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Service;
using QLDuAn.Web.Models;
using System.Net.Http;
using AutoMapper;
using System.Collections.Generic;
using QLDuAn.Model.Models;
using System.Linq;
using System;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/kh")]
    [Authorize]
    public class KhachHangController : BaseController
    {
        private IKhachHangService _ikhachHangService;
        #region
        public KhachHangController(ErrorService error , IKhachHangService iKhachHangService) :base(error)
        {
            this._ikhachHangService = iKhachHangService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request ,int page , int pageSize ,string keyword)
        {
            return CreateReponse(request, () =>
            {
                var model = _ikhachHangService.GetAll(keyword);
                var query = model.Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<KhachHang>, IEnumerable<KhachHangViewModel>>(query);

                Paginnation<KhachHangViewModel> pagination = new Paginnation<KhachHangViewModel>
                {
                    items = responseData,
                    Page = page,
                    TotalPage = Convert.ToInt32(Math.Ceiling((double)model.Count() / pageSize)),
                    TotalCount = model.Count()
                };
                return request.CreateResponse(System.Net.HttpStatusCode.OK, pagination); ;
            });
        }

        [Route("getcustomer")]
        [HttpGet]
        public HttpResponseMessage GetAllCustomer(HttpRequestMessage request)
        {
            return CreateReponse(request, () =>
            {
                var model = _ikhachHangService.GetAll();
                var responseData = Mapper.Map<IEnumerable<KhachHang>, IEnumerable<KhachHangViewModel>>(model);
                return request.CreateResponse(System.Net.HttpStatusCode.OK, responseData); ;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Created(HttpRequestMessage request , KhachHangViewModel khachHangVM)
        {
            return CreateReponse(request, () => {
                HttpResponseMessage response;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ModelState);
                }else
                {
                    var khachHang = new KhachHang();
                    khachHang.UpdateKhachHang(khachHangVM);
                    _ikhachHangService.Add(khachHang);
                    _ikhachHangService.save();
                    response = request.CreateResponse(System.Net.HttpStatusCode.Created, khachHang);
                }
               
                return response;
            });
            
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request , int id)
        {
            return CreateReponse(request, ()=> {
                HttpResponseMessage response;

                if(id != null)
                {
                    _ikhachHangService.Delete(id);
                    _ikhachHangService.save();
                    response = request.CreateResponse(System.Net.HttpStatusCode.Accepted, id);
                }else
                {
                    response = request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                }

                return response;
            });

        }

        [HttpGet]
        [Route("getbyid")]
        public HttpResponseMessage GetById( HttpRequestMessage request , int id)
        {
            return CreateReponse(request, () => {
                HttpResponseMessage response;

                if (id != null)
                {
                  var khachhang = _ikhachHangService.GetById(id);
                    response = request.CreateResponse(System.Net.HttpStatusCode.OK, khachhang);
                }
                else
                {
                    response = request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                }

                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Updated(HttpRequestMessage request, KhachHangViewModel khachHangVM)
        {
            return CreateReponse(request, () => {
                HttpResponseMessage response;

                if (ModelState.IsValid)
                {
                    var Oldkhachhang = _ikhachHangService.GetById(khachHangVM.ID);
                    Oldkhachhang.UpdateKhachHang(khachHangVM);
                    _ikhachHangService.Update(Oldkhachhang);
                    _ikhachHangService.save();
                    response = request.CreateResponse(System.Net.HttpStatusCode.Accepted,khachHangVM);
                }
                else
                {
                    response = request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                }

                return response;
            });
        }
    }
}