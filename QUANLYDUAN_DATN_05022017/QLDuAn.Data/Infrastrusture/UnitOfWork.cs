using QLDuAn.Data;

namespace QLDuAn.Data.Infrastrusture
{
    public class UnitOfWork : IUnitOfWork
    {
        private QLDuAnDbContext dbContext;

        private readonly IDbFactory _dbFactory;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public QLDuAnDbContext DbContext
        {
            get { return dbContext ?? (dbContext = _dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}