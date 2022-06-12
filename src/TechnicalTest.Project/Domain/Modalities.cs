using System;

namespace TechnicalTest.Project.Domain
{ 
    public class Modality 
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual string Detail { get; set; }

    }

    public class PaymentModality : Modality 
    {
        public string Price { get; set; }

        public override string Detail { get; set; }
    }

    public class TreatmentModality : Modality 
    {
        public string Description { get; set; }
        public override string Detail { get; set; }
    }
}