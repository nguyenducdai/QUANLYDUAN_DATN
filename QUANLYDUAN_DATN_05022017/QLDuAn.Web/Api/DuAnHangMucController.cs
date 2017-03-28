using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using Newtonsoft.Json.Linq;
using QLDuAn.Model.Models;
using QLDuAn.Web.Infastructure.Extentions;
using QLDuAn.Web.Models;
using AutoMapper;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/duanhangmuc")]
    public class DuAnHangMucController : BaseController
    {
        private IThamGiaService _thamGiaService;
        private IDuAnHangMucService _duAnHangMucService;

        public DuAnHangMucController(ErrorService errorService, IThamGiaService thamGiaService, IDuAnHangMucService duAnHangMucService) : base(errorService)
        {
            this._thamGiaService = thamGiaService;
            this._duAnHangMucService = duAnHangMucService;
        }

        [Route("created")]
        [HttpPost]
        public HttpResponseMessage Created(HttpRequestMessage request, JObject objectJson)
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
                    dynamic json = objectJson;
                    JObject jsonDaHm = json.HangMucDa;
                    JArray jsonArrThamGia = json.ThamGia;
                    var HangMucDa = jsonDaHm.ToObject<HangMucDuAnViewModel>();

                    var newDuAnHangMuc = new DuAnHangMuc();
                    newDuAnHangMuc.UpdateDuAnHangMuc(HangMucDa);
                    var model = _duAnHangMucService.Add(newDuAnHangMuc);
                    _duAnHangMucService.Save();

                    List<ThamGiaViewModel> listThamgia = new List<ThamGiaViewModel>();
                    foreach (var item in jsonArrThamGia)
                    {
                        JObject jo = JObject.FromObject(item);
                        var tg = item.ToObject<ThamGiaViewModel>();
                        tg.IdHangMuc = HangMucDa.IdHangMuc;
                        tg.LoaiHangMuc = HangMucDa.LoaiHangMuc;
                        tg.DiemThanhVien = Math.Round((HangMucDa.DiemHangMuc * tg.HeSoThamGia) / 100, 0);
                        listThamgia.Add(tg);
                    }

                    var reponseData = Mapper.Map<DuAnHangMuc, HangMucDuAnViewModel>(model);

                    if (reponseData != null)
                    {
                        foreach (var item in listThamgia)
                        {
                            var newThamGia = new ThamGia();
                            newThamGia.UpdateThamGia(item);
                            _thamGiaService.Add(newThamGia);
                            _thamGiaService.Save();
                        }
                    }
                    response = request.CreateResponse(HttpStatusCode.Created, reponseData);
                }
               
                return response;
            });
        }

        [HttpGet]
        [Route("gethangmucduan")]
        public HttpResponseMessage GetHangMucDuAn(HttpRequestMessage request, int id , int LoaiHangMuc)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var model = _duAnHangMucService.GetInfoByIdProject(id ,LoaiHangMuc);
                
                var reposeData = Mapper.Map<IEnumerable<DuAnHangMuc> , IEnumerable<HangMucDuAnViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request , int IdHangMuc, int IdDuAn, int IdNhomCongViec, int LoaiHangMuc)
        {
            return CreateReponse(request, () =>
            {
                HttpResponseMessage response;
                var b = _duAnHangMucService.DeleteMediate(IdHangMuc, IdDuAn, IdNhomCongViec, LoaiHangMuc);
                    response = request.CreateResponse(HttpStatusCode.Created, b);
                return response;
            });

        }

        [HttpGet]
        [Route("getSingle")]
        public HttpResponseMessage GetSingleById(HttpRequestMessage request, int IdHangMuc, int IdDuAn, int IdNhomCongViec, int LoaiHangMuc)
        {
            return CreateReponse(request ,()=> {
                HttpResponseMessage response;
                var model = _duAnHangMucService.GetSingleById(IdHangMuc,IdDuAn, IdNhomCongViec, LoaiHangMuc);
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
          
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage UpdateHangMuc(HttpRequestMessage request, JObject objectJson)
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
                    dynamic json = objectJson;
                    JObject jsonDaHm = json.HangMucDa;
                    JArray jsonArrThamGia = json.ThamGia;
                    var HangMucDa = jsonDaHm.ToObject<HangMucDuAnViewModel>();

                    var newDuAnHangMuc = _duAnHangMucService.GetSingleUpdate(HangMucDa.IdHangMuc, HangMucDa.IdDuAn, HangMucDa.LoaiHangMuc);
                    newDuAnHangMuc.UpdateDuAnHangMuc(HangMucDa);
                    _duAnHangMucService.Update(newDuAnHangMuc);
                    _duAnHangMucService.Save();

                    List<ThamGiaViewModel> listThamgia = new List<ThamGiaViewModel>();
                    foreach (var item in jsonArrThamGia)
                    {
                        JObject jo = JObject.FromObject(item);
                        var tg = item.ToObject<ThamGiaViewModel>();
                        tg.DiemThanhVien = Math.Round((HangMucDa.DiemHangMuc * tg.HeSoThamGia) / 100, 0);
                        listThamgia.Add(tg);
                    }

                    if (newDuAnHangMuc != null)
                    {
                        foreach (var item in listThamgia)
                        {
                            var newThamGia = _thamGiaService.GetByIdHm(item.IdHangMuc, item.IdDuAn, item.LoaiHangMuc, item.IdNhanVien);
                            if (newThamGia != null)
                            {
                                newThamGia.UpdateThamGia(item);
                                _thamGiaService.Update(newThamGia);
                                _thamGiaService.Save();
                            }
                            else
                            {
                                var ThamGia = new ThamGia();
                                ThamGia.UpdateThamGia(item);
                                _thamGiaService.Add(ThamGia);
                                _thamGiaService.Save();
                            }

                        }

                      

                    }
                    response = request.CreateResponse(HttpStatusCode.Created, "Update success");
                }
                return response;
            });
        }

        
    } 
}
