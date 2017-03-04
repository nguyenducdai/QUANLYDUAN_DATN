using QLDuAn.Web.Infastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QLDuAn.Service;
using QLDuAn.Web.Models;
using QLDuAn.Model.Models;
using QLDuAn.Web.Infastructure.Extentions;
using AutoMapper;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/duan")]
    public class DuAnController : BaseController
    {
        private IDuAnService _daService;

        public DuAnController(ErrorService errorService, IDuAnService daService) : base(errorService)
        {
            this._daService = daService;
        }
        
        [HttpGet]
        [Route("created")]
        public HttpResponseMessage created(HttpRequestMessage request, DuAnViewModel duAnVM)
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
                    var newDuan = new DuAn();
                    newDuan.UpdateDuAn(duAnVM);
                    var model = _daService.Add(newDuan);
                    var responseData = Mapper.Map<DuAn, DuAnViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });

        }


    }
}
