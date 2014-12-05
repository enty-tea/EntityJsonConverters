using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using EntyTea.JsonConverters;

namespace EntyTea.JsonConverters.UnitTests.Tests
{
    public class TestEntityJsonParserBase
    {
        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJsonString_Sample(Type parserType)
        {
            var jsonParser = (IEntityJsonParser<SampleEntity>)Activator.CreateInstance(parserType);
            var json = @"{
  ""id"": 2,
  ""name"": ""test"",
  ""createdOnString"": ""2002-02-02"",
  ""createdByUserIdentifier"": ""dec17ee8-25da-48e0-b560-8e02a7b639d0""
}";
            var actual = jsonParser.Parse(json);
            AssertSampleEntityIsEqualToExpected(actual);
        }

        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJsonString_Null(Type parserType)
        {
            var jsonParser = (IEntityJsonParser<SampleEntity>)Activator.CreateInstance(parserType);
            var actual = jsonParser.Parse((string)null);
            Assert.IsNull(actual);
        }

        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJsonStringAsObject_Sample(Type parserType)
        {
            var jsonParser = (IEntityJsonObjectParser<SampleEntity>)Activator.CreateInstance(parserType);
            var json = @"{
  ""id"": 2,
  ""name"": ""test"",
  ""createdOnString"": ""2002-02-02"",
  ""createdByUserIdentifier"": ""dec17ee8-25da-48e0-b560-8e02a7b639d0""
}";
            var actual = jsonParser.Parse((object)json);
            AssertSampleEntityIsEqualToExpected(actual);
        }

        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJsonTextReader_Sample(Type parserType)
        {
            var jsonParser = (IEntityJsonParser<SampleEntity>)Activator.CreateInstance(parserType);
            var json = @"{
  ""id"": 2,
  ""name"": ""test"",
  ""createdOnString"": ""2002-02-02"",
  ""createdByUserIdentifier"": ""dec17ee8-25da-48e0-b560-8e02a7b639d0""
}";
            var jsonReader = new StringReader(json);
            var actual = jsonParser.Parse(jsonReader);
            AssertSampleEntityIsEqualToExpected(actual);
        }

        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJsonTextReaderAsObject_Sample(Type parserType)
        {
            var jsonParser = (IEntityJsonObjectParser<SampleEntity>)Activator.CreateInstance(parserType);
            var json = @"{
  ""id"": 2,
  ""name"": ""test"",
  ""createdOnString"": ""2002-02-02"",
  ""createdByUserIdentifier"": ""dec17ee8-25da-48e0-b560-8e02a7b639d0""
}";
            var jsonReader = new StringReader(json);
            var actual = jsonParser.Parse((object)jsonReader);
            AssertSampleEntityIsEqualToExpected(actual);
        }

        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJObject_Sample(Type parserType)
        {
            var jsonParser = (IEntityJObjectParser<SampleEntity>)Activator.CreateInstance(parserType);
            var jobject = new JObject(
                new JProperty("id", 2),
                new JProperty("name", "test"),
                new JProperty("createdOnString", "2002-02-02"),
                new JProperty("createdByUserIdentifier", "dec17ee8-25da-48e0-b560-8e02a7b639d0")
            );
            var actual = jsonParser.Parse(jobject);
            AssertSampleEntityIsEqualToExpected(actual);
        }

        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJObject_Null(Type parserType)
        {
            var jsonParser = (IEntityJObjectParser<SampleEntity>)Activator.CreateInstance(parserType);
            var actual = jsonParser.Parse(null);
            Assert.IsNull(actual);
        }

        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJsonObject_Sample(Type parserType)
        {
            var jsonParser = (IEntityJsonObjectParser<SampleEntity>)Activator.CreateInstance(parserType);
            var obj = new {
              id = 2,
              name = "test",
              createdOnString = "2002-02-02",
              createdByUserIdentifier = "dec17ee8-25da-48e0-b560-8e02a7b639d0"
            };
            var actual = jsonParser.Parse(obj);
            AssertSampleEntityIsEqualToExpected(actual);
        }

        [TestCase(typeof(SampleEntityJsonParser))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void ParseJsonObject_Null(Type parserType)
        {
            var jsonParser = (IEntityJsonObjectParser<SampleEntity>)Activator.CreateInstance(parserType);
            var actual = jsonParser.Parse(null);
            Assert.IsNull(actual);
        }

        #region Shared

        private void AssertSampleEntityIsEqualToExpected(SampleEntity actual)
        {
            Assert.IsNotNull(actual);

            var expected = CreateSampleEntity();
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.CreatedOn, actual.CreatedOn);
            Assert.AreEqual(expected.CreatedByUserIdentifier, actual.CreatedByUserIdentifier);
        }

        private SampleEntity CreateSampleEntity()
        {
            return new SampleEntity
            {
                Id = 2,
                Name = "test",
                CreatedOn = new DateTime(2002, 2, 2),
                CreatedByUserIdentifier = new Guid("DEC17EE8-25DA-48E0-B560-8E02A7B639D0")
            };
        }

        #endregion
    }
}
