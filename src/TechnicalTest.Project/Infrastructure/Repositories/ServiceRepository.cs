using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Project.Domain;

namespace TechnicalTest.Project.Infrastructure.Repositories
{ 
    public class ServiceRepository : IRepository<Service> {
        private TestDbContext context;
        
        public ServiceRepository(TestDbContext context) {
            this.context = context;
        }
        Task<IEnumerable<Service>> IRepository<Service>.ListAsync(Expression<Func<Service, bool>> filter) {
            var query = from h in context.Service.Where(filter) select h;
            var returnList = query.AsEnumerable();
            return Task.FromResult(returnList);
        }
        
        Task<Service> IRepository<Service>.GetAsync() {
            throw new NotImplementedException();
        }

        Task<Service> IRepository<Service>.CreateAsync(Service entity) {

            context.Service.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }
        
        Task<Service> IRepository<Service>.UpdateAsync(Service entity) {
            var oldService = context.Service.Find(entity.Id);
            context.Service.Remove(oldService);
            context.SaveChanges();
            context.Service.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }
        


        void IRepository<Service>.Delete(Service entity)
        {
            try 
            { 
                context.Service.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}