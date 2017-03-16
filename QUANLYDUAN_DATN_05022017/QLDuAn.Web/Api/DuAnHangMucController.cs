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
    }
}
