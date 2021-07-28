using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class PensionerInput
    {
        public enum PensionType { Self,Family}
        public string NamePensioner { get; set; }
        public DateTime DOB { get; set; }
        public String PAN { get; set; }
        public String AadhaarNo { get; set; }
        public PensionType PensionSelected { get; set; }

    }
}
