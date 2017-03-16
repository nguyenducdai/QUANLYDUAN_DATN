using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLDuAn.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Adress { get; set; }

        public bool? Sex { get; set; }

        // quản lý Idenity thông qua cookie
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual IEnumerable<DuAnHangMuc> DuAnHangMuc { get; set; }
    }
}