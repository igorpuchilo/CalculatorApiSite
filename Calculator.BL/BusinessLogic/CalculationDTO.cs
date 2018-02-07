using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Calculator.BL.BusinessLogic
{
    public class CalculationDTO
    {
        public int Id { get; set; }
        public string Expression { get; set; }
        public int Result { get; set; }
        public string CreatedOn { get; set; }

        internal static readonly Expression<Func<Calculation, CalculationDTO>> Expandable = listDto => new CalculationDTO
        {
            Id = listDto.Id,
            Expression = listDto.Expression,
            Result = listDto.Result,
            CreatedOn = listDto.CreatedOn
        };
        internal static Expression<Func<Calculation, CalculationDTO>> SelectExpression = Expandable;
    }
}
