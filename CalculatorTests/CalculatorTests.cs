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
        public void Sum33_33_66()
        {
            int b = 66;
            CalculationResultItemModel model = new CalculationResultItemModel();
            ApiClient client = new ApiClient("http://localhost/Calculator/");
            ApiRequestMethod requestMethod = ApiRequestMethod.GET;
            Task<ApiResponse> response = client.MakeApiRequestAsync("/Api/values?a=33&b=33", requestMethod,null);
            ApiResponse result = response.Result;
            model = result.Deserialize<CalculationResultItemModel>();
            Assert.IsNotNull(model);
            Assert.AreEqual(b,model.Result);
        }
        public void ListAll()
        {

        }
        [TestMethod]
        public void Delete()
        {
            int Id = 22;
            string resultD = "";
            ApiClient client = new ApiClient("http://localhost/Calculator/");
            ApiRequestMethod requestMethod = ApiRequestMethod.DELETE;
            Task<ApiResponse> response = client.MakeApiRequestAsync("/Api/values?Id=22", requestMethod, null);
            ApiResponse result = response.Result;
            resultD = result.Deserialize<string>();
            Assert.IsNotNull(resultD);
            Assert.AreEqual("Delete by Id = " + Id, resultD);
        }
    }
}