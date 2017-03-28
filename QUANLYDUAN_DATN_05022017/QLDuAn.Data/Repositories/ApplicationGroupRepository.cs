using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDuAn.Model.Models;
using QLDuAn.Data.Infrastrusture;

namespace QLDuAn.Data.Repositories
{
    public interface IApplicationGroupRepository:IRepository<ApplicationGroup>
    {
        IEnumerable<ApplicationGroup> GetListGroupByIdUser(string idUser);

        IEnumerable<ApplicationUser> GetListUserByIdGroup(int idGroup);
    }
    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        public ApplicationGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ApplicationGroup> GetListGroupByIdUser(string idUser)
        {
            var query = from ag in DBContext.ApplicationGroup
                        join au in DBContext.ApplicationUserGroup
                        on ag.Id equals au.IdGroup
                        where au.IdUser == idUser
                        select ag;
            return query;
        }

        public IEnumerable<ApplicationUser> GetListUserByIdGroup(int idGroup)
        {
            var query = from au in DBContext.ApplicationGroup
                        join ag in DBContext.ApplicationUserGroup
                        on au.Id equals ag.IdGroup
                        join us in DBContext.Users
                        on ag.IdUser equals us.Id
                        where au.Id == idGroup
                        select us;
            return query;
        }
     
    }
}
