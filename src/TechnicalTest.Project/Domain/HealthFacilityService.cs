namespace TechnicalTest.Project.Domain
{
    public class HealthFacilityService
    {

        // Changed to nullable to for testing HealthFacility and Service creation
        // Id's are already present, the objects are unnecessary and will cause object cycle
        public HealthFacility? HealthFacility { get; set; }
        public string HealthFacilityId { get; set; }

        // Changed to nullable to for testing HealthFacility and Service creation
        public Service? Service { get; set; }
        public string ServiceId { get; set; }
    }
}