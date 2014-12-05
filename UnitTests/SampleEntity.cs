using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntyTea.JsonConverters.UnitTests
{
    internal class SampleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedByUserIdentifier { get; set; }
    }
}
