using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using Moq;
using EntyTea.JsonConverters;
using EntyTea.JsonConverters.Mvc.ModelBinders;

namespace EntyTea.JsonConverters.UnitTests.Mvc.ModelBinders
{
    public class TestJsonModelBinder
    {
        [Test]
        public void EntityJsonParserModelBinder_Sample()
        {
            var json = @"{
  ""id"": 2,
  ""name"": ""test"",
  ""createdOnString"": ""2002-02-02"",
  ""createdByUserIdentifier"": ""dec17ee8-25da-48e0-b560-8e02a7b639d0""
}";
            var expected = new SampleEntity
            {
                Id = 2,
                Name = "test",
                CreatedOn = new DateTime(2002, 2, 2),
                CreatedByUserIdentifier = new Guid("DEC17EE8-25DA-48E0-B560-8E02A7B639D0")
            };

            var modelBinder = new EntityJsonParserModelBinder<SampleEntity, SampleEntityJsonParser>();
            var model = modelBinder.BindModel(CreateControllerContext(json), bindingContext: null);
            var actual = model as SampleEntity;

            AssertSampleEntitiesAreEqual(expected, actual);
        }

        [Test]
        public void EntityListJObjectParserModelBinder_Sample()
        {
            var json = @"[{
  ""id"": 2,
  ""name"": ""test"",
  ""createdOnString"": ""2002-02-02"",
  ""createdByUserIdentifier"": ""dec17ee8-25da-48e0-b560-8e02a7b639d0""
},
{
  ""id"": 3,
  ""name"": ""test2""
}]";
            var expected = new[]
            {
                new SampleEntity
                {
                    Id = 2,
                    Name = "test",
                    CreatedOn = new DateTime(2002, 2, 2),
                    CreatedByUserIdentifier = new Guid("DEC17EE8-25DA-48E0-B560-8E02A7B639D0")
                },
                new SampleEntity
                {
                    Id = 3,
                    Name = "test2",
                },
            };

            var modelBinder = new EntityListJObjectParserModelBinder<SampleEntity, SampleEntityJsonParser>();
            var model = modelBinder.BindModel(CreateControllerContext(json), bindingContext: null);
            var actual = model as List<SampleEntity>;

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Length, actual.Count);
            foreach (var tuple in expected.Zip(actual, (e, a) => Tuple.Create(e, a)))
            {
                AssertSampleEntitiesAreEqual(tuple.Item1, tuple.Item2);
            }
        }

        #region Shared

        private ControllerContext CreateControllerContext(string jsonInput)
        {
            var httpRequestMock = new Mock<HttpRequestBase>();
            httpRequestMock.Setup(x => x.ContentType).Returns("application/json");
            httpRequestMock.Setup(x => x.InputStream).Returns(() => new MemoryStream(Encoding.UTF8.GetBytes(jsonInput)));

            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(x => x.Request).Returns(httpRequestMock.Object);

            var controllerContext = new ControllerContext();
            controllerContext.HttpContext = httpContextMock.Object;
            return controllerContext;
        }

        private void AssertSampleEntitiesAreEqual(SampleEntity expected, SampleEntity actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.CreatedOn, actual.CreatedOn);
            Assert.AreEqual(expected.CreatedByUserIdentifier, actual.CreatedByUserIdentifier);
        }

        #endregion
    }
}
