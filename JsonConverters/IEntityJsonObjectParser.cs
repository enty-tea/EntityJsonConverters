using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Parses an entity from a JSON object.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to parse</typeparam>
    public interface IEntityJsonObjectParser<out TEntity>
    {
        /// <summary>
        /// Returns an entity parsed from the specified JSON object.
        /// </summary>
        /// <param name="json">the JSON object to parse or null</param>
        /// <returns>the parsed entity or null</returns>
        TEntity Parse(object json);
    }
}