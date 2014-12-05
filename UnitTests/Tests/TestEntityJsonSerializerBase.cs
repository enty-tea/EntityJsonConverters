using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using EntyTea.JsonConverters;

namespace EntyTea.JsonConverters.UnitTests.Tests
{
    public class TestEntityJsonSerializerBase
    {
        [TestCase(typeof(SampleEntityJsonSerializer))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void Serialize_Sample(Type serializerType)
        {
            var jsonSerializer = (IEntityJsonSerializer<SampleEntity>)Activator.CreateInstance(serializerType);
            var actual = jsonSerializer.Serialize(CreateSampleEntity());
            var expected = @"{
  ""id"": 2,
  ""name"": ""test"",
  ""createdOnString"": ""2002-02-02"",
  ""createdByUserIdentifier"": ""dec17ee8-25da-48e0-b560-8e02a7b639d0""
}";
            Assert.AreEqual(expected, actual);
        }

        [TestCase(typeof(SampleEntityJsonSerializer))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void Serialize_Null(Type serializerType)
        {
            var jsonSerializer = (IEntityJsonSerializer<SampleEntity>)Activator.CreateInstance(serializerType);
            var actual = jsonSerializer.Serialize(null);
            Assert.IsNull(actual);
        }

        [TestCase(typeof(SampleEntityJsonSerializer))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void SerializeToReader_Sample(Type serializerType)
        {
            var jsonSerializer = (IEntityJsonSerializer<SampleEntity>)Activator.CreateInstance(serializerType);
            var actual = jsonSerializer.SerializeToReader(CreateSampleEntity());
            var expected = @"{
  ""id"": 2,
  ""name"": ""test"",
  ""createdOnString"": ""2002-02-02"",
  ""createdByUserIdentifier"": ""dec17ee8-25da-48e0-b560-8e02a7b639d0""
}";
            Assert.AreEqual(expected, actual.ReadToEnd());
        }

        [TestCase(typeof(SampleEntityJsonSerializer))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void SerializeToReader_Null(Type serializerType)
        {
            var jsonSerializer = (IEntityJsonSerializer<SampleEntity>)Activator.CreateInstance(serializerType);
            var actual = jsonSerializer.SerializeToReader(null);
            Assert.IsNull(actual);
        }

        [TestCase(typeof(SampleEntityJsonSerializer))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void SerializeToJObject_Sample(Type serializerType)
        {
            var jsonSerializer = (IEntityJObjectSerializer<SampleEntity>)Activator.CreateInstance(serializerType);
            var actual = jsonSerializer.SerializeToJObject(CreateSampleEntity());
            var expected = new JObject(
                new JProperty("id", 2),
                new JProperty("name", "test"),
                new JProperty("createdOnString", "2002-02-02"),
                new JProperty("createdByUserIdentifier", "dec17ee8-25da-48e0-b560-8e02a7b639d0")
            );
            Assert.AreEqual(expected, actual);
        }

        [TestCase(typeof(SampleEntityJsonSerializer))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void SerializeToJObject_Null(Type serializerType)
        {
            var jsonSerializer = (IEntityJObjectSerializer<SampleEntity>)Activator.CreateInstance(serializerType);
            var actual = jsonSerializer.SerializeToJObject(null);
            Assert.IsNull(actual);
        }

        [TestCase(typeof(SampleEntityJsonSerializer))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void SerializeToJsonObject_Sample(Type serializerType)
        {
            var jsonSerializer = (IEntityJsonObjectSerializer<SampleEntity>)Activator.CreateInstance(serializerType);
            var actual = jsonSerializer.SerializeToJsonObject(CreateSampleEntity());
            var expected = new {
              id = 2,
              name = "test",
              createdOnString = "2002-02-02",
              createdByUserIdentifier = "dec17ee8-25da-48e0-b560-8e02a7b639d0"
            };
            Assert.AreEqual(expected, actual);
        }

        [TestCase(typeof(SampleEntityJsonSerializer))]
        [TestCase(typeof(SampleEntityJsonConverter))]
        public void SerializeToJsonObject_Null(Type serializerType)
        {
            var jsonSerializer = (IEntityJsonObjectSerializer<SampleEntity>)Activator.CreateInstance(serializerType);
            var actual = jsonSerializer.SerializeToJsonObject(null);
            Assert.IsNull(actual);
        }

        #region Shared

        private SampleEntity CreateSampleEntity()
        {
            return new SampleEntity { 
                Id = 2, 
                Name = "test", 
                CreatedOn = new DateTime(2002, 2, 2), 
                CreatedByUserIdentifier = new Guid("DEC17EE8-25DA-48E0-B560-8E02A7B639D0")
            };
        }

        #endregion
    }
}
