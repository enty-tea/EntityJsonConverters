using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Converts an entity to JSON.
    /// </summary>
    public abstract class EntityJsonSerializerBase<TEntity> : IEntityJsonSerializer<TEntity>, IEntityJObjectSerializer<TEntity>, IEntityJsonObjectSerializer<TEntity>
    {
        /// <remarks>
        /// This is the only method that needs to be implemented to support serializing.  
        /// All of the other serialize methods call this one (sometimes indirectly).
        /// </remarks>
        public abstract object SerializeToJsonObject(TEntity entity);

        /// <remarks>
        /// This implementation creates a JObject from the return value of the <c>SerializeToJsonObject</c> method.
        /// </remarks>
        public virtual JObject SerializeToJObject(TEntity entity)
        {
            var o = SerializeToJsonObject(entity);
            if (o == null) return null;

            return JObject.FromObject(o);
        }

        /// <remarks>
        /// This implementation creates a string from the return value of the <c>SerializeToJObject</c> method.
        /// </remarks>
        public virtual string Serialize(TEntity entity)
        {
            var jobject = SerializeToJObject(entity);
            if (jobject == null) return null;

            return jobject.ToString(Newtonsoft.Json.Formatting.Indented);
        }

        /// <remarks>
        /// This implementation create a <c>TextReader</c> from the return value of <c>SerializeToJson</c>.
        /// </remarks>
        public virtual TextReader SerializeToReader(TEntity entity)
        {
            var json = Serialize(entity);
            if (json == null) return null;

            return new StringReader(json);
        }
    }
}