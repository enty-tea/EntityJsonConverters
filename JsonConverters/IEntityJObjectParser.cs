using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Parses an entity from a JObject.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to parse</typeparam>
    public interface IEntityJObjectParser<out TEntity>
    {
        /// <summary>
        /// Returns an entity parsed from the specified JObject.
        /// </summary>
        /// <param name="json">the JObject to parse or null</param>
        /// <returns>the parsed entity or null</returns>
        TEntity Parse(JObject json);
    }
}