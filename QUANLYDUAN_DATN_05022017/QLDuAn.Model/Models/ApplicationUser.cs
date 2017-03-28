using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QLDuAn.Model.IComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QLDuAn.Model.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Adress { get; set; }

        public string FullName { get; set; }

        public string Bio { get; set; }

        public bool? Sex { get; set; }

        public string Image { get; set;}

        public string Function { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime Created_at { get; set;}

        public DateTime Updatted_at { get; set; }

        // quản lý Idenity thông qua cookie
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual IEnumerable<DuAnHangMuc> DuAnHangMuc { get; set; }

        public IEnumerable<ApplicationGroup> Groups { get; set; }


    }
}