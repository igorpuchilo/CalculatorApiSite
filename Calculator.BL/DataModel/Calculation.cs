using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator
{
    public class Calculation
    {
        public int Id { get; set; }
        public string Expression { get; set; }
        public int Result { get; set; }
        public string CreatedOn { get; set; }
    }
}