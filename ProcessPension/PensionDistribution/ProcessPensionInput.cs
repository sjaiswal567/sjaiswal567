using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDistribution
{
    public class ProcessPensionInput
    {
        public enum Bankcharge {Public=500,Private=550}
        public String AadhaarNo { get; set; }
        public decimal PensionAmt { get; set; }
        public Bankcharge BankCharges { get; set; }
    }
}
