using QLDuAn.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Models;

namespace QLDuAn.Web.Controllers
{
    [Authorize]
    public class DuAnController : Controller
    {
        private IDuAnService _duAnService;
        private IThamGiaService _thamgiaService;
        private IHangMucService _hangMucService;

        public DuAnController(DuAnService duAnService ,IThamGiaService thamgiaService, IHangMucService hangMucService)
        {
            this._duAnService = duAnService;
            this._thamgiaService = thamgiaService;
            this._hangMucService = hangMucService;
        }

        // GET: DuAn
        [HttpGet]
        public ActionResult HangMucCongViec()
        {
            if (Request.IsAuthenticated)
            {
               SetViewBag();
            }
            return View();
        }

        public JsonResult GetHangMucCV(int idDuAn)
        {
            var idNhanVien = User.Identity.GetUserId();
            var model = _hangMucService.GetHangMucByIdUser(idDuAn , idNhanVien);
            var responseData = new JavaScriptSerializer().Serialize(model);
            return Json(new {
                data = responseData,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public void SetViewBag(long? idSelected = null)
        {
            var model = _duAnService.GetDaByIdUser(User.Identity.GetUserId());
            var viewVM = Mapper.Map<IEnumerable<DuAn>, IEnumerable<DuAnViewModel>>(model);
            ViewBag.ID = new SelectList(viewVM, "ID", "TenDuAn", idSelected);

        }

        public ActionResult ThongKe()
        {
            if (Request.IsAuthenticated)
            {
                SetViewBag();
            }
            return View();
        }

        public JsonResult ThongKeHangMc(int idDuAn)
        {
            var idNhanVien = User.Identity.GetUserId();
            var modelDirect = _thamgiaService.GetIncomeByIdUser(idDuAn, idNhanVien , 0);
            var modelInDirect = _thamgiaService.GetIncomeByIdUser(idDuAn, idNhanVien , 1);
            var responseDataDirect = new JavaScriptSerializer().Serialize(modelDirect);
            var responseDataInDirect = new JavaScriptSerializer().Serialize(modelInDirect);

            return Json(new
            {
                dataDirect = responseDataDirect,
                dataInDirect = responseDataInDirect,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult XepHangDoanhThu(int idDuAn)
        {
            var modelDirect = _thamgiaService.GetIncomeById(idDuAn , 0);
            var modelInDirect = _thamgiaService.GetIncomeById(idDuAn, 1);
            var responseDataDirect = new JavaScriptSerializer().Serialize(modelDirect);
            var responseDataInDirect = new JavaScriptSerializer().Serialize(modelInDirect);

            return Json(new
            {
                dataDirect = responseDataDirect,
                dataInDirect = responseDataInDirect,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult updateStatus(int id, bool status)
        {
            var oldHangMuc = _hangMucService.getByID(id);
            oldHangMuc.TrangThai = status;
            _hangMucService.Update(oldHangMuc);
            _hangMucService.save();
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}