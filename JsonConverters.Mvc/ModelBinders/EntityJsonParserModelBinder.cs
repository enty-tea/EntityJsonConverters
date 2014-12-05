using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EntyTea.JsonConverters.Mvc.ModelBinders
{
    /// <summary>
    /// An implementation of <c>IModelBinder</c> that parses a JSON string into an entity.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to parse</typeparam>
    /// <typeparam name="TEntityJsonParser">the parser to create the entity from a JSON string</typeparam>
    public class EntityJsonParserModelBinder<TEntity, TEntityJsonParser> : JsonModelBinderBase
        where TEntity : class
        where TEntityJsonParser : IEntityJsonParser<TEntity>
    {
        protected override object BindModel(TextReader jsonReader)
        {
            var dependencyResolver = System.Web.Mvc.DependencyResolver.Current;
            var jsonParserObject = dependencyResolver.GetService(typeof(TEntityJsonParser))
                ?? Activator.CreateInstance(typeof(TEntityJsonParser));
            var jsonParser = (TEntityJsonParser)jsonParserObject;
            return jsonParser.Parse(jsonReader);
        }
    }
}
