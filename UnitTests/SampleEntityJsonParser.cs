using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntyTea.JsonConverters;
using Newtonsoft.Json.Linq;

namespace EntyTea.JsonConverters.UnitTests
{
    internal class SampleEntityJsonParser : EntityJsonParserBase<SampleEntity>
    {
        public override SampleEntity Parse(JObject json)
        {
            if (json == null) return null;
            return new SampleEntity
            {
                Id = json.PropertyValueOrDefault<int>("id"),
                Name = json.PropertyStringValueOrDefault("name"),
                CreatedOn = json.PropertyValueOrDefault<DateTime>("createdOnString"),
                CreatedByUserIdentifier = json.PropertyValueOrDefault<Guid>("createdByUserIdentifier"),
            };
        }
    }

}
