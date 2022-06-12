using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Project.Domain;

namespace TechnicalTest.Project.Infrastructure.Repositories
{ 
    public class PractitionerRepository : IRepository<Practitioner> {
        private TestDbContext context;
        
        public PractitionerRepository(TestDbContext context) {
            this.context = context;
        }
        Task<IEnumerable<Practitioner>> IRepository<Practitioner>.ListAsync(Expression<Func<Practitioner, bool>> filter) {
            var query = from h in context.Practitioner.Where(filter) select h;
            var returnList = query.AsEnumerable();
            return Task.FromResult(returnList);
        }
        
        Task<Practitioner> IRepository<Practitioner>.GetAsync() {
            throw new NotImplementedException();
        }   

        Task<Practitioner> IRepository<Practitioner>.CreateAsync(Practitioner entity) {

            context.Practitioner.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }
        
        Task<Practitioner> IRepository<Practitioner>.UpdateAsync(Practitioner entity) {
            var oldfac = context.Practitioner.Find(entity.Id);
            context.Practitioner.Remove(oldfac);
            context.SaveChanges();
            context.Practitioner.AddAsync(entity);
            context.SaveChanges();
            return Task.FromResult(entity);
        }

        void IRepository<Practitioner>.Delete(Practitioner entity)
        {
            try 
            { 
                context.Practitioner.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}