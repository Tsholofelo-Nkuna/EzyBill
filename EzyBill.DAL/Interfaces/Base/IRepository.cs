using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EzyBill.DAL.Interfaces.Base
{
    public interface IRepository<TEntity, TUserKey> where TEntity : IBaseEntity<TUserKey>
    {
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        public void Insert(List<TEntity> entities, TUserKey userId);
        public void Update(List<TEntity> entities, TUserKey userId);
        public void Delete(IEnumerable<Guid> identifiers, TUserKey userId);
        public int SaveChanges();
    }
}
