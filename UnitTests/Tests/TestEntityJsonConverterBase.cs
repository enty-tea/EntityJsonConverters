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
    public class TestEntityJsonConverterBase
    {
        private SampleEntityJsonConverter jsonConverter;

        [SetUp]
        public void SetUp()
        {
            jsonConverter = new SampleEntityJsonConverter();
        }

        [Test]
        public void SerializeThenParse_Sample()
        {
            var entity = CreateSampleEntity();
            var json = jsonConverter.Serialize(entity);
            var parsedEntity = jsonConverter.Parse(json);
            AssertSampleEntitiesAreEqual(entity, parsedEntity);
        }

        [Test]
        public void SerializeToReaderThenParse_Sample()
        {
            var entity = CreateSampleEntity();
            var jsonReader = jsonConverter.SerializeToReader(entity);
            var parsedEntity = jsonConverter.Parse(jsonReader);
            AssertSampleEntitiesAreEqual(entity, parsedEntity);
        }

        [Test]
        public void SerializeToJObjectThenParse_Sample()
        {
            var entity = CreateSampleEntity();
            var jobject = jsonConverter.SerializeToJObject(entity);
            var parsedEntity = jsonConverter.Parse(jobject);
            AssertSampleEntitiesAreEqual(entity, parsedEntity);
        }

        [Test]
        public void SerializeToJsonObjectThenParse_Sample()
        {
            var entity = CreateSampleEntity();
            var obj = jsonConverter.SerializeToJsonObject(entity);
            var parsedEntity = jsonConverter.Parse(obj);
            AssertSampleEntitiesAreEqual(entity, parsedEntity);
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
