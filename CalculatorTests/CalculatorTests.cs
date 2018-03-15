using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Controllers;
using System.Net.Http;
using Calculator.Models;
using CalculatorTests.Client;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public async Task Sum33_33_66()
        { 
            CalculationResultItemModel model = new CalculationResultItemModel();
            ApiClient client = new ApiClient();
            string url = "/Api/values?a=33&b=33";
            CalculationResultItemModel response = new CalculationResultItemModel();
            response = await client.Get_Sum(url);
            Assert.IsNotNull(model);
            Assert.AreEqual(66,model.Result);
        }
        public void ListAll()
        {

        }
        public void Delete()
        {

        }
    }
}
