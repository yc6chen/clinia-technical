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
    public class FacilityController : ControllerBase
    {
        private IRepository<HealthFacility> _facilityRepo;
        private IRepository<HealthFacilityService> _facilityServiceRepo;
        private IRepository<Practitioner> _practitionerRepo;
        private IRepository<PractitionerService> _practitionerServiceRepo;
        private IRepository<Service> _serviceRepo;
        public FacilityController(
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

        [Route("get_facility")]
        [HttpGet]
        public async Task<HealthFacility> GetFacilityAsync()
        {

            return await _facilityRepo.GetAsync();
        }

        // post will timeout on the response due to the object cycle, but new entry will be successfully inserted
        [Route("post_facility")]
        [HttpPost]
        public async Task<OkObjectResult> PostFacilityAsync([FromBody]HealthFacility facility)
        {
            try
            {
                var newFacility = await _facilityRepo.CreateAsync(facility);
                return Ok(newFacility);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            
        }

        /**
         * Unable to fully test due to System.Text.Json.JsonException: A possible object cycle was detected 
         * which is not supported. This can either be due to a cycle or if the object depth is larger than 
         * the maximum allowed depth of 32.
         * 
         */
        [Route("get_facility_id")]
        [HttpGet]
        public async Task<Dictionary<string, object>> GetFacilityByIdAsync(string id)
        {
            Dictionary<string, object> returnDict = new Dictionary<string, object>();
            var facility = (await _facilityRepo.ListAsync(facility => facility.Id == id)).FirstOrDefault();
            returnDict.Add("Facility", facility);

            var practitioner = (await _practitionerRepo.ListAsync(prac => prac.HealthFacility.Id == id)).FirstOrDefault();
            returnDict.Add("Practitioner", practitioner);

            // does not use the IEnumerable of HealthFacilityServices or PractitionerServices because when 
            // HealthFacility and Practitioner and Service objects are retrieved from the Database, the HealthFacilityServices 
            // and PractitionerServices are null
            var facRelServices = (await _facilityServiceRepo.ListAsync(facService => facService.HealthFacilityId == id));
            var facilityServiceList = new List<Service>();
            foreach (var rel in facRelServices) {
                var facServices = await _serviceRepo.ListAsync(service => service.Id == rel.ServiceId);
                facilityServiceList.AddRange(facServices);
            }
            returnDict.Add("FacilityServices", facilityServiceList);

            var pracServiceList = new List<Service>();
            var pracRelServices = (await _practitionerServiceRepo.ListAsync(pracService => pracService.PractitionerId == practitioner.Id));
            foreach (var rel in facRelServices)
            {
                var pracServices = await _serviceRepo.ListAsync(service => service.Id == rel.ServiceId);
                pracServiceList.AddRange(pracServices);
            }

            returnDict.Add("PractitionerServices", pracServiceList);
            return returnDict;
        }

        //list and counts will return successful status codes
        [Route("list_facility_name")]
        [HttpGet]
        public async Task<IEnumerable<HealthFacility>> ListFacilityByNameAsync(string name)
        {
            return await _facilityRepo.ListAsync(facility => facility.Name == name);
        }

        [Route("count_facility_name")]
        [HttpGet]
        public async Task<int> CountFacilityByNameAsync(string name)
        {
            return (await _facilityRepo.ListAsync(facility => facility.Name == name)).Count();
        }

        [Route("list_facility_phone")]
        [HttpGet]
        public async Task<IEnumerable<HealthFacility>> ListFacilityByphoneAsync(string? phoneNumber)
        {
            return await _facilityRepo.ListAsync(facility => facility.PhoneNumber == phoneNumber);
        }

        [Route("count_facility_phone")]
        [HttpGet]
        public async Task<int> CountFacilityByPhoneAsync(string? phoneNumber)
        {
            return (await _facilityRepo.ListAsync(facility => facility.PhoneNumber == phoneNumber)).Count();
        }

        [Route("list_facility_street_num")]
        [HttpGet]
        public async Task<IEnumerable<HealthFacility>> ListFacilityByStreetNumberAsync(string? streetNumber)
        {
            return await _facilityRepo.ListAsync(facility => facility.StreetNumber == streetNumber);
        }

        [Route("count_facility_street_num")]
        [HttpGet]
        public async Task<int> CountFacilityByStreetNumberAsync(string? streetNumber)
        {
            return (await _facilityRepo.ListAsync(facility => facility.StreetNumber == streetNumber)).Count();
        }


        [Route("list_facility_street")]
        [HttpGet]
        public async Task<IEnumerable<HealthFacility>> ListFacilityByStreetAsync(string? streetName)
        {
            return await _facilityRepo.ListAsync(facility => facility.StreetName == streetName);
        }

        [Route("count_facility_street")]
        [HttpGet]
        public async Task<int> CountFacilityByStreetAsync(string? streetName)
        {
            return (await _facilityRepo.ListAsync(facility => facility.StreetName == streetName)).Count();
        }

        [Route("list_facility_city")]
        [HttpGet]
        public async Task<IEnumerable<HealthFacility>> ListFacilityByCityAsync(string? city)
        {
            return await _facilityRepo.ListAsync(facility => facility.City == city);
        }

        [Route("count_facility_city")]
        [HttpGet]
        public async Task<int> CountFacilityByCityAsync(string? city)
        {
            return (await _facilityRepo.ListAsync(facility => facility.City == city)).Count();
        }

        [Route("list_facility_open_status")]
        [HttpGet]
        public async Task<IEnumerable<HealthFacility>> ListFacilityByOpenStatusAsync(bool? isOpen)
        {
            return await _facilityRepo.ListAsync(facility => facility.IsOpen == isOpen);
        }

        [Route("count_facility_open_status")]
        [HttpGet]
        public async Task<int> CountFacilityByOpenStatusAsync(bool? isOpen)
        {
            return (await _facilityRepo.ListAsync(facility => facility.IsOpen == isOpen)).Count();
        }

        [Route("list_facility_type")]
        [HttpGet]
        public async Task<IEnumerable<HealthFacility>> ListFacilityByTypeAsync(string type)
        {
            return await _facilityRepo.ListAsync(facility => facility.Type == type);
        }

        [Route("count_facility_type")]
        [HttpGet]
        public async Task<int> CountFacilityByTypeAsync(string type)
        {
            return (await _facilityRepo.ListAsync(facility => facility.Type == type)).Count();
        }

        // unable to fully debug in time
        [Route("put_facility")]
        [HttpPut]
        public async Task<HealthFacility> UpdateFacility([FromBody]HealthFacility facility)
        {
            try { 
                return await _facilityRepo.UpdateAsync(facility);
            }
            catch (Exception ex)
            {
                throw ex;
            }
}

        [Route("delete_facility")]
        [HttpDelete]
        public async Task DeleteFacility(string id)

        {
            try
            {
                var facility = (await _facilityRepo.ListAsync(facility => facility.Id == id)).FirstOrDefault();
                _facilityRepo.Delete(facility);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
