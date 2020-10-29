using System.Threading.Tasks;

namespace Aucxis.Eprw.Reporting.Dataservice
{
    public interface IDatabaseScope
    {
        AppDbContext DbContext { get; set; }

        /// <summary>
        /// Saves pending changes asynchronously.
        /// </summary>
        Task<int> SaveAsync();

        int Save();
    }
}
