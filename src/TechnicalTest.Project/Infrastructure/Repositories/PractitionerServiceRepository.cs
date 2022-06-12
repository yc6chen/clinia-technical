using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Project.Domain;

namespace TechnicalTest.Project.Infrastructure.Repositories
{ 
    public class PractitionerServiceRepository : IRepository<PractitionerService> {
        private TestDbContext context;
        
        public PractitionerServiceRepository(TestDbContext context) {
            this.context = context;
        }
        Task<IEnumerable<PractitionerService>> IRepository<PractitionerService>.ListAsync(Expression<Func<PractitionerService, bool>> filter) {
            var query = from h in context.PractitionerService.Where(filter) select h;
            var returnList = query.AsEnumerable();
            return Task.FromResult(returnList);
        }
        
        Task<PractitionerService> IRepository<PractitionerService>.GetAsync() {
            throw new NotImplementedException();
        }

        Task<PractitionerService> IRepository<PractitionerService>.CreateAsync(PractitionerService entity) {

            context.PractitionerService.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }
        
        Task<PractitionerService> IRepository<PractitionerService>.UpdateAsync(PractitionerService entity) {
            var oldfac = context.PractitionerService.Find(entity.PractitionerId, entity.ServiceId);
            context.PractitionerService.Remove(oldfac);
            context.SaveChanges();
            context.PractitionerService.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }


        void IRepository<PractitionerService>.Delete(PractitionerService entity)
        {
            try 
            { 
                context.PractitionerService.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}