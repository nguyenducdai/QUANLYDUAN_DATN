using QLDuAn.Data.Infrastrusture;
using QLDuAn.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDuAn.Data.Repositories
{
    public interface IThamGiaRepository : IRepository<ThamGia>
    {

    }
    public class ThamGiaRepository : RepositoryBase<ThamGia>, IThamGiaRepository
    {
        public ThamGiaRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
