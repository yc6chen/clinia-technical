using System.Collections.Generic;

namespace TechnicalTest.Project.Domain
{
    public class Practitioner
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int? Age { get; set; }
        
        public string PracticeNumber { get; set; }
        
        public HealthFacility HealthFacility { get; set; }
        
        public IEnumerable<PractitionerService> PractitionerServices { get; set; }
    }
}