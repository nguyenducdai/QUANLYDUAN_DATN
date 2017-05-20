using System;
using System.Collections.Generic;

namespace QLDuAn.Web.Models
{
    public class ApplicationUserViewModel
    {
        public string Id{ get; set; }

        public int AccessFailedCount { get; set; }

        public string Adress { get; set; }

        public string Bio { get; set; }

        public string FullName { get; set; }

        public bool? Sex { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set;}

        public bool PhoneNumberConfirmed { get; set; }

        public string SecurityStamp { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string UserName { get; set; }

        public string Image { get; set; }

        public string Function { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime Startdate { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Updatted_at { get; set; }

        public virtual IEnumerable<HangMucViewModel> HangMucViewModel { get; set; }

        public IEnumerable<ApplicationGroupViewModel> Groups { get; set; }

        public IEnumerable<DuAnViewModel> DuAn { get; set; }

    }
}