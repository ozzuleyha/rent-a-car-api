using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarProject.Models
{
    public class RentInformation
    {
        public int RentId{ get; set; }
        public int RentCarId { get; set; }
        public int RentCustomerId { get; set; }
        public float RentPrice { get; set; }
        public float CarStartKm { get; set; }
        public float CarFinalKm { get; set; }
        public int SituationId { get; set; }


    }
}
