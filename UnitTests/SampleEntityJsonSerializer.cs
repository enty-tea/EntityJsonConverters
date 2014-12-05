using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntyTea.JsonConverters;

namespace EntyTea.JsonConverters.UnitTests
{
    internal class SampleEntityJsonSerializer : EntityJsonSerializerBase<SampleEntity>
    {
        public override object SerializeToJsonObject(SampleEntity entity)
        {
            if (entity == null) return null;
            return new
            {
                id = entity.Id,
                name = entity.Name,
                createdOnString = entity.CreatedOn.ToString("yyyy-MM-dd"),
                createdByUserIdentifier = entity.CreatedByUserIdentifier.ToString()
            };
        }
    }
}
