using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDuAn.Web.Models;
using QLDuAn.Service;
using QLDuAn.Common;
using AutoMapper;
using QLDuAn.Model.Models;
using QLDuAn.Web.Infastructure.Core;
using System.Web.Script.Serialization;

namespace QLDuAn.Web.Controllers
{
    public class HomeController : Controller
    {
        private IThongBaoService _thongBaoService;

        public HomeController(IThongBaoService thongBaoService)
        {
            this._thongBaoService = thongBaoService;
        }

        public ActionResult Index(int page = 1)
        {
            //int total = 0;
            //int pageSize =Convert.ToInt32(ConfigHelper.GetValueByKey("pageSize"));
            //int maxPage = Convert.ToInt32(ConfigHelper.GetValueByKey("maxPage"));
            //var model = _thongBaoService.GetListPaging(page,pageSize,out total);
            //var reponseData = Mapper.Map<IEnumerable<ThongBao>, IEnumerable<ThongBaoViewModel>>(model);
            //int totalPage = Convert.ToInt32(Math.Ceiling((decimal)total/ pageSize));

            //var pagination = new Paginnation<ThongBaoViewModel>
            //{
            //    items = reponseData,
            //    Page = page,
            //    TotalPage = totalPage,
            //    TotalCount = total,
            //    MaxPage = maxPage
            //};

            return View();
        }

        public JsonResult GetAllAlert(string keyword)
        {
            var data = new JavaScriptSerializer().Serialize(_thongBaoService.GetAll(keyword));
            return Json(new{
                    data = data,
                    status = true
                    },JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Site(int page =1)
        {
           
            var AlNow = _thongBaoService.GetAlertByDateNow();
            ViewBag.AlNowVB = Mapper.Map<IEnumerable<ThongBao>, IEnumerable<ThongBaoViewModel>>(AlNow);

            int total = 0;
            int pageSize = Convert.ToInt32(ConfigHelper.GetValueByKey("pageSize"));
            int maxPage = Convert.ToInt32(ConfigHelper.GetValueByKey("maxPage"));
          
                var model = _thongBaoService.GetListPaging(page, pageSize, out total).Where(x => x.Created_at.Value.ToShortDateString() != DateTime.Now.ToShortDateString()).OrderByDescending(x => x.Id);

    
            var reponseData = Mapper.Map<IEnumerable<ThongBao>, IEnumerable<ThongBaoViewModel>>(model);
            int totalPage = Convert.ToInt32(Math.Ceiling((decimal)total / pageSize));

            var pagination = new Paginnation<ThongBaoViewModel>
            {
                items = reponseData,
                Page = page,
                TotalPage = totalPage,
                TotalCount = total,
                MaxPage = maxPage
            };

          
            return View(pagination);
        }

        public ActionResult GetDetail(int id)
        {
            var data = new JavaScriptSerializer().Serialize(_thongBaoService.GetById(id));
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int id)
        {
            var model = _thongBaoService.GetById(id);
            var respondata = Mapper.Map<ThongBao, ThongBaoViewModel>(model);

            List<FileInfo> listFile = new JavaScriptSerializer().Deserialize<List<FileInfo>>(respondata.MoreFile);
            ViewBag.ListFile = listFile;
            return View(respondata);
        }

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}