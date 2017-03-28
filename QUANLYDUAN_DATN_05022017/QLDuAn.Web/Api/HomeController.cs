using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using System.Web.Http;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : BaseController
    {
        private ErrorService _errorService;

        public HomeController(ErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string Home()
        {
            return "hello";
        }
    }
}