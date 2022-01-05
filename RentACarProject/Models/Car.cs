using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarProject.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int CompanyId { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public float RentPrice { get; set; }
        public int RequiredLicenseAge { get; set; }
        public int SeatingCapacity { get; set; }
        public string Airbag { get; set; }
        public int SituationId { get; set; }
        public string PhotoFileName { get; set; }
    }
}
