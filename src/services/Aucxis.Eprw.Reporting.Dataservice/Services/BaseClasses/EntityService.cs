using Aucxis.Eprw.Reporting.Dataservice.Entities;
using Aucxis.Eprw.Reporting.Dataservice.Services.Validation;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aucxis.Eprw.Reporting.Dataservice.Services
{
    public abstract class EntityService<TEntity, TModel> : IEntityService<TModel>
        where TEntity : class, IEntity, new()
        where TModel : class
    {
        protected readonly IDatabaseScope _database;
        protected readonly IGenericRepository<TEntity> _entityRepository;
        protected Mapper _mapper;

        public IGenericRepository<TEntity> EntityRepository
        {
            get
            {
                return _entityRepository;
            }
        }

        public string SourceAPI { get; set; }

        protected EntityService(IDatabaseScope database, IGenericRepository<TEntity> entityRepository)
        {
            _database = database;
            _entityRepository = entityRepository;
        }

        #region IEntityService
        public async virtual Task<List<TModel>> GetAllAsync()
        {
            var list = new List<TModel>();

            //ProjectTo would also load all child objects
            await GetAllEntities(_entityRepository.AsReadOnly())
                .ForEachAsync(e => list.Add(Mapper.Map<TModel>(e)));

            return list;
        }

        public async virtual Task<TModel> GetAsync(long id)
        {
            return await GetSingleEntityQuery(_entityRepository.AsReadOnly(), id)
                .ProjectTo<TModel>()
                .FirstAsync();
        }

        public async virtual Task<TModel> CreateAsync(TModel model)
        {
            model.RejectInvalid();

            var entity = new TEntity();
            UpdateEntity(entity, model);

            await AdditionalCreateValidation(model, entity);

            _entityRepository.Insert(entity);
            await _database.SaveAsync();

            return Mapper.Map<TModel>(entity);
        }

        public async virtual Task<TModel> UpdateAsync(long id, TModel model)
        {
            model.RejectInvalid();

            var entity = await GetEntityByIdAsync(_entityRepository.GetAll(), id);
            entity.RejectNotFound();

            await AdditionalUpdateValidation(model, entity);

            UpdateEntity(entity, model);
            await _database.SaveAsync();

            return Mapper.Map<TModel>(entity);
        }

        public async virtual Task DeleteAsync(long id)
        {
            var entity = await GetEntityByIdAsync(_entityRepository.GetAll(), id);
            entity.RejectNotFound();

            await AdditionalDeleteValidation(entity);

            _entityRepository.Delete(entity);

            await _database.SaveAsync();
        }

        protected virtual Task<TEntity> GetEntityByIdAsync(IQueryable<TEntity> query, long id)
        {
            return GetSingleEntityQuery(query, id).FirstOrDefaultAsync();
        }

        public virtual Task<TEntity> GetReadonlyEntityByIdAsync(long id)
        {
            return GetSingleEntityQuery(_entityRepository.AsReadOnly(), id).FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> GetAllEntities(IQueryable<TEntity> query)
        {
            return query;
        }

        protected virtual void UpdateEntity(TEntity entity, TModel model)
        {
            Mapper.Map<TModel, TEntity>(model, entity);
        }

        protected async virtual Task AdditionalCreateValidation(TModel model, TEntity entity)
        {
            await Task.FromResult<object>(null);
        }

        protected async virtual Task AdditionalUpdateValidation(TModel model, TEntity entity)
        {
            await Task.FromResult<object>(null);
        }

        protected async virtual Task AdditionalDeleteValidation(TEntity entity)
        {
            await Task.FromResult<object>(null);
        }

        protected virtual IQueryable<TEntity> GetSingleEntityQuery(IQueryable<TEntity> query, long id)
        {
            return GetAllEntities(query).Where(e => e.Id == id);
        }

        #endregion
    }
}
