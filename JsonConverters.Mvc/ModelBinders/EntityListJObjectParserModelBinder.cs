using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EntyTea.JsonConverters.Mvc.ModelBinders
{
    /// <summary>
    /// An implementation of <c>IModelBinder</c> that parses a JSON string into a list of entities.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to parse</typeparam>
    /// <typeparam name="TEntityJObjectParser">the parser to create the entities from a JSON string</typeparam>
    public class EntityListJObjectParserModelBinder<TEntity, TEntityJObjectParser> : JsonModelBinderBase
        where TEntity : class
        where TEntityJObjectParser : IEntityJObjectParser<TEntity>
    {
        protected override object BindModel(TextReader jsonReader)
        {
            var dependencyResolver = System.Web.Mvc.DependencyResolver.Current;
            var jobjectParserObject = dependencyResolver.GetService(typeof(TEntityJObjectParser))
                ?? Activator.CreateInstance(typeof(TEntityJObjectParser));
            var jobjectParser = (TEntityJObjectParser)jobjectParserObject;
            using (var jsonTextReader = new JsonTextReader(jsonReader) { DateParseHandling = DateParseHandling.DateTimeOffset })
            {
                return JArray.Load(jsonTextReader).OfType<JObject>().Select(jobjectParser.Parse).ToList();
            }
        }
    }
}
