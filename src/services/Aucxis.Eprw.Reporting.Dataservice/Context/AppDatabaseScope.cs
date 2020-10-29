using System.Threading.Tasks;

namespace Aucxis.Eprw.Reporting.Dataservice
{
    public class AppDatabaseScope : IDatabaseScope
    {
        public AppDbContext DbContext { get; set;  }

        public AppDatabaseScope(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task<int> SaveAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }
    }
}
