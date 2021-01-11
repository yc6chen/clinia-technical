using System.Collections.Generic;

namespace TechnicalTest.Project.Domain
{
    public class Service
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public IEnumerable<HealthFacilityService> HealthFacilityServices { get; set; }
        
        public IEnumerable<PractitionerService> PractitionerServices { get; set; }
    }
}