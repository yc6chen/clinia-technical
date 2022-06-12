using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TechnicalTest.Project.Domain;
using TechnicalTest.Project.Infrastructure.Repositories;

namespace TechnicalTest.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PractitionerController : ControllerBase
    {
        private IRepository<HealthFacility> _facilityRepo;
        private IRepository<HealthFacilityService> _facilityServiceRepo;
        private IRepository<Practitioner> _practitionerRepo;
        private IRepository<PractitionerService> _practitionerServiceRepo;
        private IRepository<Service> _serviceRepo;

        public PractitionerController(
            IRepository<HealthFacility> facilityrepository,
            IRepository<HealthFacilityService> facilityServiceRepo,
            IRepository<Practitioner> practitionerRepo,
            IRepository<PractitionerService> practitionerServiceRepo,
            IRepository<Service> serviceRepo)
        {
            _facilityRepo = facilityrepository;
            _facilityServiceRepo = facilityServiceRepo;
            _practitionerRepo = practitionerRepo;
            _practitionerServiceRepo = practitionerServiceRepo;
            _serviceRepo = serviceRepo;
        }

        [Route("get_practitioner")]
        [HttpGet]
        public async Task<Practitioner> GetPractitionerAsync()
        {

            return await _practitionerRepo.GetAsync();
        }

        // post will timeout on the response due to the object cycle, but new entry will be successfully inserted
        [Route("post_practitioner")]
        [HttpPost]
        public async Task<Practitioner> PostPractitionerAsync([FromBody]Practitioner practitioner)
        {

            return await _practitionerRepo.CreateAsync(practitioner);
        }
        /**
         * Unable to fully test due to System.Text.Json.JsonException: A possible object cycle was detected 
         * which is not supported. This can either be due to a cycle or if the object depth is larger than 
         * the maximum allowed depth of 32.
         * 
         */
        [Route("get_practitioner_id")]
        [HttpGet]
        public async Task<Dictionary<string, object>> GetPractitionerByIdAsync(string id)
        {
            Dictionary<string, object> returnDict = new Dictionary<string, object>();
            var practitioner = (await _practitionerRepo.ListAsync(practitioner => practitioner.Id == id)).FirstOrDefault();
            returnDict.Add("Practitioner", practitioner);

            var facility = (await _facilityRepo.ListAsync(fac => fac.Id == practitioner.HealthFacility.Id)).FirstOrDefault();
            returnDict.Add("Facility", facility);

            // does not use the IEnumerable of HealthFacilityServices or PractitionerServices because when 
            // HealthFacility and Practitioner and Service objects are retrieved from the Database, the HealthFacilityServices 
            // and PractitionerServices are null
            var facRelServices = (await _facilityServiceRepo.ListAsync(facService => facService.HealthFacilityId == facility.Id));
            var facilityServiceList = new List<Service>();
            foreach (var rel in facRelServices)
            {
                var facServices = await _serviceRepo.ListAsync(service => service.Id == rel.ServiceId);
                facilityServiceList.AddRange(facServices);
            }
            returnDict.Add("FacilityServices", facilityServiceList);

            var pracServiceList = new List<Service>();
            var pracRelServices = (await _practitionerServiceRepo.ListAsync(pracService => pracService.PractitionerId == id));
            foreach (var rel in facRelServices)
            {
                var pracServices = await _serviceRepo.ListAsync(service => service.Id == rel.ServiceId);
                pracServiceList.AddRange(pracServices);
            }

            returnDict.Add("PractitionerServices", pracServiceList);
            return returnDict;
        }

        //list and counts will return successful status codes
        [Route("list_practitioner_firstname")]
        [HttpGet]
        public async Task<IEnumerable<Practitioner>> ListPractitionerByFirstNameAsync(string firstname)
        {
            return await _practitionerRepo.ListAsync(practitioner => practitioner.FirstName == firstname);
        }

        [Route("count_practitioner_firstname")]
        [HttpGet]
        public async Task<int> CountPractitionerByFirstNameAsync(string firstname)
        {
            return (await _practitionerRepo.ListAsync(practitioner => practitioner.FirstName == firstname)).Count();
        }

        [Route("list_practitioner_lastname")]
        [HttpGet]
        public async Task<IEnumerable<Practitioner>> ListPractitionerByLastNameAsync(string lastname)
        {
            return await _practitionerRepo.ListAsync(practitioner => practitioner.LastName == lastname);
        }

        [Route("count_practitioner_lastname")]
        [HttpGet]
        public async Task<int> CountPractitionerByLastNameAsync(string lastname)
        {
            return (await _practitionerRepo.ListAsync(practitioner => practitioner.LastName == lastname)).Count();
        }

        [Route("list_practitioner_age")]
        [HttpGet]
        public async Task<IEnumerable<Practitioner>> ListPractitionerByAgeAsync(int? age)
        {
            return await _practitionerRepo.ListAsync(practitioner => practitioner.Age == age);
        }

        [Route("count_practitioner_age")]
        [HttpGet]
        public async Task<int> CountPractitionerByAgeAsync(int? age)
        {
            return (await _practitionerRepo.ListAsync(practitioner => practitioner.Age == age)).Count();
        }


        [Route("list_practitioner_number")]
        [HttpGet]
        public async Task<IEnumerable<Practitioner>> ListPractitionerByPracticeNumberAsync(string practiceNumber)
        {
            return await _practitionerRepo.ListAsync(practitioner => practitioner.PracticeNumber == practiceNumber);
        }

        [Route("count_practitioner_number")]
        [HttpGet]
        public async Task<int> CountPractitionerByPracticeNumber(string practiceNumber)
        {
            return (await _practitionerRepo.ListAsync(practitioner => practitioner.PracticeNumber == practiceNumber)).Count();
        }

        // unable to fully debug in time
        [Route("put_practitioner")]
        [HttpPut]
        public async Task<Practitioner> UpdatePractitioner([FromBody]Practitioner practitioner)
        {
            return await _practitionerRepo.UpdateAsync(practitioner);
        }

        [Route("delete_practitioner")]
        [HttpDelete]
        public async Task DeletePractitioner(string id)
        {
            try { 
                var practitioner = (await _practitionerRepo.ListAsync(practitioner => practitioner.Id == id)).FirstOrDefault();
                _practitionerRepo.Delete(practitioner);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
