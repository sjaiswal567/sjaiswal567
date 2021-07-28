using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDetail
{
    public class PensionDetail
    {
        public struct BankDetails
        {
            public enum BankType { Public,Private}
            public string BankName { get; set; }
            public string BankAccountNo { get; set; }
            public BankType BankChoice { get; set; }
        }
        public enum PensionType { Self, Family }
        public string NamePensioner { get; set; }
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
        public PensionType PensionSelected { get; set; }
        public decimal SalaryEarned { get; set; }
        public decimal Allowances { get; set; }
        public BankDetails BankInfo { get; set; }
    }
}
