using EzyBill.DAL.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Repositories.Base
{
    public class Repository<TEnity> : IRepository<TEnity> where TEnity : class, IBaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEnity> _set;
        public Repository(DbContext context) {
          _dbContext = context;
          _set = context.Set<TEnity>();
           
        }
        public virtual void Delete(IEnumerable<Guid> identifiers, Guid userId)
        {
            var removed = this
            ._set.Where(x => identifiers.Contains(x.Id))
            .ToList();
             removed.ForEach(x =>
                {
                    x.IsDeleted = true;
                });
            this.Update(removed, userId);
        }

        public IQueryable<TEnity> Get(Expression<Func<TEnity, bool>> predicate)
        {
            return this._set.AsNoTracking().Where(predicate);
        }

        public void Insert(List<TEnity> entities, Guid userId)
        {
            entities.ForEach(x =>
            {
               x.CreatedOn = DateTime.Now;
               x.CreatedBy = userId;
            });
            this._set.AddRange(entities);
        }

        public int SaveChanges()
        {
            return this._dbContext.SaveChanges();
        }

        public void Update(List<TEnity> entities, Guid userId)
        {
            entities.ForEach(x =>
            {
                x.ModifiedBy = userId;
                x.ModifiedOn = DateTime.Now;
            });
           this._set.UpdateRange(entities);
        }
    }
}
