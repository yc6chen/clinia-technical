using System.Collections.Generic;

namespace TechnicalTest.Project.Domain
{
    public class HealthFacility
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string? PhoneNumber { get; set; }
        
        public string? StreetNumber { get; set; }
        
        public string? StreetName { get; set; }

        public string? City { get; set; }
        
        public bool? IsOpen { get; set; }
        
        public string Type { get; set; }
        
        public IEnumerable<HealthFacilityService> HealthFacilityServices { get; set; }
    }
}