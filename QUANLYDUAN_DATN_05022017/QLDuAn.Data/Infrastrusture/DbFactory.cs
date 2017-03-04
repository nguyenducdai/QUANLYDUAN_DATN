using QLDuAn.Data;

namespace QLDuAn.Data.Infrastrusture
{
    public class DbFactory : Dispostable, IDbFactory
    {
        private QLDuAnDbContext dbContext;

        public QLDuAnDbContext Init()
        {
            return dbContext ?? (dbContext = new QLDuAnDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}