using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Controllers;
using System.Net.Http;
using Calculator.Models;
using CalculatorTests.HttpClient;
using System.Net; 
namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Sum33_33_66()
        { 
            //public IHttpActionResult Get(int a, int b)
            //{
            //    return Ok(_factory.GetResult(_caclulator.Add(a, b)));
            //}

            int a = 33;
            int b = 33;
            CalculationResultItemModel model = new CalculationResultItemModel();

            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetById(42))
                .Returns(new Product { Id = 42 });

            var controller = new Products2Controller(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(42);
            var contentResult = actionResult as OkNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);


            //SomeModelObject model = new SomeModelObject();
            //var httpClientMock = new Mock<IHttpClient>();
            //httpClientMock.Setup(c => c.GetAsync<SomeModelObject>(It.IsAny<string>()))
            //    .Returns(() => Task.FromResult(model));

            //_httpClient = httpClientMock.Object;

            //var client = new Connection(_httpClient);

            //// Assuming doSomething uses the client to make
            //// a request for a model of type SomeModelObject
            //client.doSomething();
        }
        public void ListAll()
        {

        }
        public void Delete()
        {

        }
    }
}
