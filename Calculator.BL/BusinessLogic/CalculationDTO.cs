using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BL.BusinessLogic
{
    class ResultDto
    {
        public int Id { get; set; }
        public string Expression { get; set; }
        public int Result { get; set; }
        public string CreatedOn { get; set; }
    }
}
