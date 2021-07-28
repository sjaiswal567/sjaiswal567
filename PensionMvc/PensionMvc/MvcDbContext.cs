using PensionMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PensionMvc
{
    public class MvcDbContext :DbContext
    {
        public MvcDbContext() : base("df")
        { }
        public DbSet<PensionerInput> PensionerInputs {get;set;}
        public DbSet<PensionDetails> PensionDetailss { get; set; }

    }
}