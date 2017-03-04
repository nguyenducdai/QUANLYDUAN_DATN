using Microsoft.AspNet.Identity.Owin;
using QLDuAn.Service;
using QLDuAn.Web.Infastructure.Core;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static QLDuAn.Web.App_Start.IdentityConfig;

namespace QLDuAn.Web.Api
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ErrorService errorService, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(errorService)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // POST: /Account/Login
        [HttpPost]
        [Route("login")]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, string e, string p)
        {
            var result = await SignInManager.PasswordSignInAsync(e, p, false, shouldLockout: false);
            if (!result.Equals(SignInStatus.Success))
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            return request.CreateResponse(HttpStatusCode.OK, result);

        }
    }
}