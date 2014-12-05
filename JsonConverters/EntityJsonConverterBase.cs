using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Parses and serializes an entity to and from a JSON format.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to convert</typeparam>
    public abstract class EntityJsonConverterBase<TEntity>
        : IEntityJsonParser<TEntity>, IEntityJObjectParser<TEntity>, IEntityJsonObjectParser<TEntity>,
        IEntityJsonSerializer<TEntity>, IEntityJObjectSerializer<TEntity>, IEntityJsonObjectSerializer<TEntity>
    {
        #region Serializer

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

        #endregion

        #region Parser

        /// <remarks>
        /// This is the only method that needs to be implemented to support parsing.  
        /// All of the other parse methods call this one (sometimes indirectly).
        /// </remarks>
        public abstract TEntity Parse(JObject json);

        /// <remarks>
        /// This implementation delegates to <c>Parse(JObject)</c>, or to one of the other <c>Parse</c> methods if there is a better type match.
        /// </remarks>
        public virtual TEntity Parse(object json)
        {
            if (json == null) return default(TEntity);
            if (json is string) return Parse((string)json);
            if (json is TextReader) return Parse((TextReader)json);
            if (json is JObject) return Parse((JObject)json);

            var jObject = JObject.FromObject(json);
            return Parse(jObject);
        }

        /// <remarks>
        /// This implementation delegates to the <c>Parse(TextReader)</c> method.
        /// </remarks>
        public virtual TEntity Parse(string json)
        {
            if (json == null) return default(TEntity);

            using (var stringReader = new StringReader(json))
            {
                return Parse(stringReader);
            }
        }

        /// <remarks>
        /// This implementation parses the string to a JObject and delegates to the <c>Parse(JObject)</c> method.
        /// </remarks>
        public virtual TEntity Parse(TextReader jsonReader)
        {
            if (jsonReader == null) return default(TEntity);

            using (var reader = new JsonTextReader(jsonReader) { DateParseHandling = DateParseHandling.DateTimeOffset })
            {
                var jobject = JObject.Load(reader);
                if (reader.Read() && reader.TokenType != JsonToken.Comment)
                {
                    throw new JsonReaderException("Additional text found in JSON string after parsing content.");
                }
                return Parse(jobject);
            }
        }

        #endregion
    }
}
