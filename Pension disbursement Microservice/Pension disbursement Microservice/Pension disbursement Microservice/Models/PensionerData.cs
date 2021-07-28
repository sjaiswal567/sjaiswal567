using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pension_disbursement_Microservice.Models
{
    public class PensionerData
    {
        public string panNumber { get; set; }
        public int amount { get; set; } 
        public Dictionary<string, int> typeofbank {get;set;}
    }
}
