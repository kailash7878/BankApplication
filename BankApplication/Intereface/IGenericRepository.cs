using BankApplication.Common;
using System.Linq.Expressions;

namespace BankApplication.Intereface
{
    public interface IGenericRepository<T> where T : class
    {
        T Select(Expression<Func<T, bool>> expression);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T,bool>> expression);
        void Add(T Entity);
        void AddRange(List<T> Entitys);
        void Update(T Entity);
        void Delete(T entity);
        void DeleteRange(List<T> entitys);
        IQueryable<T> GetAllwithInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
