using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Project.Domain;

namespace TechnicalTest.Project.Infrastructure.Repositories
{ 
    public class HealthFacilityServiceRepository : IRepository<HealthFacilityService> {

        private TestDbContext context;
        
        public HealthFacilityServiceRepository(TestDbContext context) {
            this.context = context;
        }
        Task<IEnumerable<HealthFacilityService>> IRepository<HealthFacilityService>.ListAsync(Expression<Func<HealthFacilityService, bool>> filter) {
            var query = from h in context.HealthFacilityService.Where(filter) select h;
            var returnList = query.AsEnumerable();
            return Task.FromResult(returnList);
        }
        
        Task<HealthFacilityService> IRepository<HealthFacilityService>.GetAsync() {
            throw new NotImplementedException();
        }

        Task<HealthFacilityService> IRepository<HealthFacilityService>.CreateAsync(HealthFacilityService entity)
        {
            context.HealthFacilityService.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }
        
        Task<HealthFacilityService> IRepository<HealthFacilityService>.UpdateAsync(HealthFacilityService entity) {
            var oldfac = context.HealthFacilityService.Find(entity.HealthFacilityId, entity.ServiceId);
            context.HealthFacilityService.Remove(oldfac);
            context.SaveChanges();
            context.HealthFacilityService.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }
        

        void IRepository<HealthFacilityService>.Delete(HealthFacilityService entity)
        {
            try { 
                context.HealthFacilityService.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}