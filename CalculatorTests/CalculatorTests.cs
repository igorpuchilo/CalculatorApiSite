using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Controllers;
using System.Net.Http;
using Calculator.Models;
using CalculatorTests.Client;
using System.Net;
using Newtonsoft.Json;
namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Sum33_33_66()
        { 
            CalculationResultItemModel model = new CalculationResultItemModel();
            ApiClient client = new ApiClient();
            string url = "http://localhost:50993/Api/values?a=33&b=33";
            HttpResponseMessage response = new HttpResponseMessage();
            response = client.Get(url);
            response.Content.ReadAsStringAsync().ContinueWith((readTask) =>
            {
                model = (CalculationResultItemModel)JsonConvert.DeserializeObject(readTask.Result);
            });
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
