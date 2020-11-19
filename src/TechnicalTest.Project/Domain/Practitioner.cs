using System;
using System.Collections.Generic;

namespace TechnicalTest.Domain
{
    public class Practitioner
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PracticeNumber { get; set; }
        
        public int Age { get; set; }
        
        public IEnumerable<Service> Services { get; set; }
        
        public HealthFacility HealthFacility { get; set; }
    }
}