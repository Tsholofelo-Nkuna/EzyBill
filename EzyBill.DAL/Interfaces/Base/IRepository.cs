using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Interfaces.Base
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        public void Insert(List<TEntity> entities, Guid userId);
        public void Update(List<TEntity> entities, Guid userId);
        public void Delete(IEnumerable<Guid> identifiers, Guid userId);
        public int SaveChanges();
    }
}
