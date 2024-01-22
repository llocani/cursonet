using basededatos;
using Microsoft.EntityFrameworkCore;
using Stores.IContext;
using System.Linq.Expressions;

namespace Data.Queries
{
    public class EntityQuery<T> : IEntityQuery<T>
        where T : class
    {
        private readonly CosasContext _serviceContext;
        public EntityQuery(CosasContext serviceContext)
        {
            _serviceContext = serviceContext;
        }
        public T AddItem(T item)
        {
            try
            {
                var itemnuevo = _serviceContext.Set<T>().Add(item).Entity;
                _serviceContext.SaveChanges();
                return itemnuevo;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public void AddItems(List<T> itemsList)
        {
            _serviceContext.Set<T>().AddRange(itemsList);
            _serviceContext.SaveChanges();
        }
        public void DeleteItem(T item)
        {
            _serviceContext.Set<T>().Remove(item);
            _serviceContext.SaveChanges();
        }
        public void DeleteItems(List<T> itemsList)
        {
            _serviceContext.Set<T>().RemoveRange(itemsList);
            _serviceContext.SaveChanges();
        }
        public  T? GetFirstOrDefaultUntracked(Expression<Func<T, bool>> funcPred)
        {
            return _serviceContext.Set<T>().Where(funcPred).AsNoTracking().FirstOrDefault();
        }
        public void UpdateItem(T item)
        {
            _serviceContext.Set<T>().Update(item);
            _serviceContext.SaveChanges();
        }
        public void UpdateItems(List<T> itemsList)
        {
            _serviceContext.Set<T>().UpdateRange(itemsList);
        }
        public IQueryable<T> GetAll()
        {
            try
            {
            var result = _serviceContext.Set<T>();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public IQueryable<T> GetAllUntracked()
        {
            return _serviceContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> GetByCriteria(Expression<Func<T, bool>> funcPred)
        {
            return _serviceContext.Set<T>().Where(funcPred);
        }
        public IQueryable<T> GetByCriteriaUntracked(Expression<Func<T, bool>> funcPred)
        {
            return _serviceContext.Set<T>().Where(funcPred).AsNoTracking();
        }
        public List<T> ToList(IQueryable<T> itemsList)
        {
            return itemsList.ToList();
        }
        public T? GetFirstOrDefault(Expression<Func<T, bool>> funcPred)
        {
            return _serviceContext.Set<T>().Where(funcPred).FirstOrDefault();
        }
    }
}