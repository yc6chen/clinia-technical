using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Project.Domain;

namespace TechnicalTest.Project.Infrastructure.Repositories
{ 
    public class HealthFacilityRepository : IRepository<HealthFacility> {

        private TestDbContext context;
        
        public HealthFacilityRepository(TestDbContext context) {
            this.context = context;
        }
        Task<IEnumerable<HealthFacility>> IRepository<HealthFacility>.ListAsync(Expression<Func<HealthFacility, bool>> filter) {
            var query = from h in context.HealthFacility.Where(filter) select h;
            var returnList = query.AsEnumerable();
            return Task.FromResult(returnList);
        }
        
        Task<HealthFacility> IRepository<HealthFacility>.GetAsync() {
            return context.HealthFacility.FirstOrDefaultAsync();
        }

        Task<HealthFacility> IRepository<HealthFacility>.CreateAsync(HealthFacility entity) {
            
            context.HealthFacility.AddAsync(entity);
            try
            {
                context.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            return Task.FromResult(entity);
        }
        
        Task<HealthFacility> IRepository<HealthFacility>.UpdateAsync(HealthFacility entity) {

            var oldfac = context.HealthFacility.Find(entity.Id);
            context.HealthFacility.Remove(oldfac);
            context.SaveChanges();
            context.HealthFacility.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }

        void IRepository<HealthFacility>.Delete(HealthFacility entity) 
        {
            try
            {
                context.HealthFacility.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}