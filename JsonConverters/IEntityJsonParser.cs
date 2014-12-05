using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EntyTea.JsonConverters
{
    /// <summary>
    /// Parses an entity from a JSON string.
    /// </summary>
    /// <typeparam name="TEntity">the type of entity to parse</typeparam>
    public interface IEntityJsonParser<out TEntity>
    {
        /// <summary>
        /// Returns an entity parsed from the specified JSON string.
        /// </summary>
        /// <param name="json">the JSON to parse or null</param>
        /// <returns>the parsed entity or null</returns>
        TEntity Parse(string json);

        /// <summary>
        /// Returns an entity parsed from the specified JSON text reader.
        /// </summary>
        /// <param name="jsonReader">the reader of the JSON to parse or null</param>
        /// <returns>the parsed entity or null</returns>
        TEntity Parse(TextReader jsonReader);
    }
}