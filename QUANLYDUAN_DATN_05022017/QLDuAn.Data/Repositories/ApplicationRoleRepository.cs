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
        IEnumerable<ApplicationRole> GetRoleByGroupId(int groupId);
    }
    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationRole> GetRoleByGroupId(int groupId)
        {
            var query = from appRole in DBContext.ApplicationRole
                        join appRoleGroup in DBContext.ApplicationRoleGroup
                        on appRole.Id equals appRoleGroup.RoleId
                        where appRoleGroup.GroupId == groupId
                        select appRole;
            return query;
        }
    }
}
