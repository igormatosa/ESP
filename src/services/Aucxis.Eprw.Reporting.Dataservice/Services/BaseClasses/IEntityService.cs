using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aucxis.Eprw.Reporting.Dataservice.Services
{
    public interface IEntityService<TModel>
         where TModel : class
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetAsync(long id);
        Task<TModel> CreateAsync(TModel model);
        Task<TModel> UpdateAsync(long id, TModel model);
        Task DeleteAsync(long id);
    }
}
