using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACarProject.Models
{
    public class Customer : User
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public double CustomerTCKN { get; set; }
        public DateTime CustomerBirthDay { get; set; }
        public DateTime CustomerDrivingLicenseDate { get; set; }

    }
}
