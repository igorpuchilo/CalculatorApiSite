using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Calculator.Models;
using Calculator.BL.BusinessLogic;
namespace Calculator.Factory
{
    public class CalculationFactory
    {
     
        public CalculationResultItemModel GetResult(CalculationDTO dto)
        {
            CalculationResultItemModel Model = new CalculationResultItemModel();
            Model.Expression = dto.Expression;
            Model.Result = dto.Result;
            return Model;
        }
        public CalculationResultModel GetResult(CalculationDTO[] dto)
        {
            CalculationResultModel Models = new CalculationResultModel();
            Models.Results = dto.Select(GetResult).ToArray();
            return Models;
        }
    }
}