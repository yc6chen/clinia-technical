using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TechnicalTest.Project.Domain;

namespace TechnicalTest.Project.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
        
        Task<T> GetAsync();

        Task<T> CreateAsync(T entity);
        
        Task<T> UpdateAsync(T entity);

        
        void Delete(T entity);


    }
}