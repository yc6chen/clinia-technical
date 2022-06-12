namespace TechnicalTest.Project.Domain
{
    public class PractitionerService
    {
        // Changed to nullable to for testing Practitioner and service creation
        // Id's are already present, the objects are unnecessary and will cause object cycle
        public Practitioner? Practitioner { get; set; }
        public string PractitionerId { get; set; }
        
        // Changed to nullable to for testing Practitioner and service creation
        public Service? Service { get; set; }
        public string ServiceId { get; set; }
    }
}