using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuAn.Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {

    }
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
