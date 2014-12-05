using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Parses an entity from a JSON string.
    /// </summary>
    public abstract class EntityJsonParserBase<TEntity> : IEntityJsonParser<TEntity>, IEntityJObjectParser<TEntity>, IEntityJsonObjectParser<TEntity>
    {
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

            var jobject = JObject.FromObject(json);
            return Parse(jobject);
        }

        /// <remarks>
        /// This implementation delegates <c>Parse(TextReader)</c>.
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
        /// This implementation delegates <c>Parse(JObject)</c>.
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
    }
}