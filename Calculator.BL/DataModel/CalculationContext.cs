using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Calculator
{
    public class CalculationContext : DbContext
    {
        public CalculationContext()
           : base("DbConnection")
        { }
        public DbSet<Calculation> Calculation { get; set; }
    }
}