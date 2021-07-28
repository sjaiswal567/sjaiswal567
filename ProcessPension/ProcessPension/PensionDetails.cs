using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class PensionDetails
    {
        public enum PensionType { Self, Family }
        public string NamePensioner { get; set; }
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
        public string PensionSelected { get; set; }
        public int PensionAmt { get; set; }

    }
}
