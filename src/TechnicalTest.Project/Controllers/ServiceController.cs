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
    public class ServiceController : ControllerBase
    {
        private IRepository<HealthFacility> _facilityRepo;
        private IRepository<HealthFacilityService> _facilityServiceRepo;
        private IRepository<Practitioner> _practitionerRepo;
        private IRepository<PractitionerService> _practitionerServiceRepo;
        private IRepository<Service> _serviceRepo;

        public ServiceController(
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

        [Route("get_service")]
        [HttpGet]
        public async Task<Service> GetServiceAsync()
        {

            return await _serviceRepo.GetAsync();
        }

        [Route("get_facility_service")]
        [HttpGet]
        public async Task<HealthFacilityService> GetFacilityServiceAsync()
        {

            return await _facilityServiceRepo.GetAsync();
        }

        [Route("get_practitioner_service")]
        [HttpGet]
        public async Task<PractitionerService> GetPractitionerServiceAsync()
        {

            return await _practitionerServiceRepo.GetAsync();
        }

        /**
         * Unable to fully test due to System.Text.Json.JsonException: A possible object cycle was detected 
         * which is not supported. This can either be due to a cycle or if the object depth is larger than 
         * the maximum allowed depth of 32.
         * 
         */
        [Route("get_service_id")]
        [HttpGet]
        public async Task<Dictionary<string, object>> GetServiceByIdAsync(string id)
        {
            Dictionary<string, object> returnDict = new Dictionary<string, object>();
            var Service = (await _serviceRepo.ListAsync(service => service.Id == id)).FirstOrDefault();
            returnDict.Add("Service", Service);

            // does not use the IEnumerable of HealthFacilityServices or PractitionerServices because when 
            // HealthFacility and Practitioner and Service objects are retrieved from the Database, the HealthFacilityServices 
            // and PractitionerServices are null
            var facRelServices = (await _facilityServiceRepo.ListAsync(facService => facService.ServiceId == id));
            var facilityList = new List<HealthFacility>();
            foreach (var rel in facRelServices)
            {
                var facilities = await _facilityRepo.ListAsync(facility => facility.Id == rel.HealthFacilityId);
                facilityList.AddRange(facilities);
            }
            returnDict.Add("Facilities", facilityList);

            var pracList = new List<Practitioner>();
            var pracRelServices = (await _practitionerServiceRepo.ListAsync(pracService => pracService.ServiceId == id));
            foreach (var rel in pracRelServices)
            {
                var practitioners = await _practitionerRepo.ListAsync(practitioner => practitioner.Id == rel.PractitionerId);
                pracList.AddRange(practitioners);
            }

            returnDict.Add("Practitioners", pracList);
            return returnDict;
        }

        // post will timeout on the response due to the object cycle, but new entry will be successfully inserted
        [Route("post_service")]
        [HttpPost]
        public async Task<Service> PostPractitionerAsync([FromBody]Service service)
        {

            return await _serviceRepo.CreateAsync(service);
        }

        // post will timeout on the response due to the object cycle, but new entry will be successfully inserted
        [Route("post_practitioner_service")]
        [HttpPost]
        public async Task<PractitionerService> PostPractitionerServiceAsync(string practitionerId, string serviceId)
        {
            //var Service = (await _serviceRepo.ListAsync(service => service.Id == serviceId)).FirstOrDefault();
            //var Practitioner = (await _practitionerRepo.ListAsync(practitioner => practitioner.Id == practitionerId)).FirstOrDefault();
            var practitionerService = new PractitionerService();
            practitionerService.PractitionerId = practitionerId;
            //practitionerService.Practitioner = Practitioner;
            practitionerService.ServiceId = serviceId;
            //practitionerService.Service = Service;
            return await _practitionerServiceRepo.CreateAsync(practitionerService);
        }

        // post will timeout on the response due to the object cycle, but new entry will be successfully inserted
        [Route("post_facility_service")]
        [HttpPost]
        public async Task<HealthFacilityService> PostFacilityServiceAsync(string facilityId, string serviceId)
        {
            var Service = (await _serviceRepo.ListAsync(service => service.Id == serviceId)).FirstOrDefault();
            var Facility = (await _facilityRepo.ListAsync(facility => facility.Id == facilityId)).FirstOrDefault();
            var facilityService = new HealthFacilityService();
            facilityService.HealthFacilityId = facilityId;
            //facilityService.HealthFacility = Facility;
            facilityService.ServiceId = serviceId;
            //facilityService.Service = Service;
            return await _facilityServiceRepo.CreateAsync(facilityService);
        }

        //list and counts will return successful status codes
        [Route("list_service_name")]
        [HttpGet]
        public async Task<IEnumerable<Service>> ListServiceByNameAsync(string name)
        {
            return await _serviceRepo.ListAsync(service => service.Name == name);
        }

        [Route("count_service_name")]
        [HttpGet]
        public async Task<int> CountServiceByNameAsync(string name)
        {
            return (await _serviceRepo.ListAsync(practitioner => practitioner.Name == name)).Count();
        }

        [Route("list_service_description")]
        [HttpGet]
        public async Task<IEnumerable<Service>> ListServiceByDescriptionAsync(string description)
        {
            return await _serviceRepo.ListAsync(practitioner => practitioner.Description == description);
        }

        [Route("count_service_description")]
        [HttpGet]
        public async Task<int> CountServiceByDescriptionAsync(string description)
        {
            return (await _serviceRepo.ListAsync(practitioner => practitioner.Description == description)).Count();
        }

        // unable to fully debug in time
        [Route("put_service")]
        [HttpPut]
        public async Task<Service> UpdateService([FromBody] Service service)
        {
            return await _serviceRepo.UpdateAsync(service);
        }

        [Route("delete_service")]
        [HttpDelete]
        public async Task DeleteService(string id)
        {
            var service = (await _serviceRepo.ListAsync(service => service.Id == id)).FirstOrDefault();
            _serviceRepo.Delete(service);

        }

        [Route("delete_facility_service")]
        [HttpDelete]
        public async Task DeleteFacilityService(string facilityId, string serviceId)
        {
            var service = (await _facilityServiceRepo.ListAsync(service => service.HealthFacilityId == facilityId && service.ServiceId == serviceId)).FirstOrDefault();
            _facilityServiceRepo.Delete(service);

        }

        [Route("delete_practitioner_service")]
        [HttpDelete]
        public async Task DeletePractitionerService(string practitionerId, string serviceId)
        {
            try 
            { 
                var service = (await _practitionerServiceRepo.ListAsync(service => service.PractitionerId == practitionerId && service.ServiceId == serviceId)).FirstOrDefault();
                _practitionerServiceRepo.Delete(service);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
