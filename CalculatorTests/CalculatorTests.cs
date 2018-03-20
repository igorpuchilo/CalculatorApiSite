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
        private CalculationResultModel models = new CalculationResultModel();
        private CalculationResultItemModel model = new CalculationResultItemModel();
        private ApiClient client = new ApiClient("http://localhost/Calculator/");
        private ApiRequestMethod requestMethod;
        private Task<ApiResponse> response;
        private ApiResponse result;

        [TestMethod]
        public void Sum33_33_66()
        {
            requestMethod = ApiRequestMethod.GET;
            response = client.MakeApiRequestAsync("/Api/values?a=22&b=22", requestMethod);
            result = response.Result;
            model = result.Deserialize<CalculationResultItemModel>();
            Assert.IsNotNull(model);
            Assert.AreEqual(44,model.Result);
        }
        [TestMethod]
        public void ListAll()
        {
            requestMethod = ApiRequestMethod.GET;
            response = client.MakeApiRequestAsync("/Api/values?All=list", requestMethod);
            result = response.Result;
            models = result.Deserialize<CalculationResultModel>();
            Assert.IsNotNull(models);
        }
        [TestMethod]
        public void Delete()
        {
            int Id = 36;
            string resultD = "";
            requestMethod = ApiRequestMethod.DELETE;
            response = client.MakeApiRequestAsync("/Api/values?Id="+Id, requestMethod);
            result = response.Result;
            resultD = result.Deserialize<string>();
            Assert.IsNotNull(resultD);
            Assert.AreEqual("Delete by Id = " + Id, resultD);
        }
    }
}